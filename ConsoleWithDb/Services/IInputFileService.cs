using System.Collections.Generic;
using SystemWrapper.Interface;
using Training.Data;

namespace ConsoleWithDb.Services
{
    public interface IInputFileService
    {
        void CreateInputFile(IFileInfo fileInfo);
        List<IFileInfo> GetFilesFromInputPath();
        List<InputFileGetDto> GetInputFilesFromDb();
    }
}