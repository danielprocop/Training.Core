using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemWrapper.Interface;
using Training.Core;
using Training.Data;

namespace ConsoleWithDb.Services
{
    public class InputFileService : IInputFileService
    {
        private readonly string _pathInput;
        private readonly string _pathBackup;
        private readonly IContextFactory _contextFactory;
        private readonly IInputFileRepository _inputFileRepository;
        private readonly IDirectoryInfo _directoryInfo;
        private readonly IFile _file;

        public InputFileService(IOptions<Paths> options
            , IContextFactory contextFactory
            , IInputFileRepository inputFileRepository
            , IDirectoryInfo directoryInfo
            , IFile file)
        {
            _pathBackup = options.Value.BACKUP;
            _pathInput = options.Value.InputPath;
            this._contextFactory = contextFactory;
            this._inputFileRepository = inputFileRepository;
            this._directoryInfo = directoryInfo;
            this._file = file;
        }

        public List<IFileInfo> GetFilesFromInputPath()
        {
            _directoryInfo.Inizialize(_pathInput);
            return _directoryInfo.GetFiles().ToList();
        }
        public void CreateInputFile(IFileInfo fileInfo)
        {
            try
            {
                if (fileInfo is null)
                {
                    throw new ArgumentNullException(nameof(fileInfo));
                }


                InputFileInsertDto dto = new InputFileInsertDto(
                        fileInfo.Name
                        , _file.ReadAllBytes(fileInfo.FullName)
                        , fileInfo.Extension
                        , Status.New);

                using IContext context = _contextFactory.Create();
                _inputFileRepository.Insert(dto, context);
                _file.Move(fileInfo.FullName, Path.Combine(_pathBackup, fileInfo.Name));
                context.Commit();
            }
            catch (Exception)
            {

                throw;
            }
           


        }

        public List<InputFileGetDto> GetInputFilesFromDb()
        {
            using IContext context = _contextFactory.Create();
            return _inputFileRepository.GetByStatusAndOrderByCreationDate(Status.New, context).ToList();
        }
    }
}
