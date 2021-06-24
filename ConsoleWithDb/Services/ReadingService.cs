using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Training.Core;
using Training.Data;
using Training.Data.Reading;

namespace ConsoleWithDb.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IContextFactory _contextFactory;
        private readonly IInputFileRepository _inputFileRepository;
        private readonly IReadingsImportService _readingsImportService;
        private readonly IReadingRepository _readingRepository;

        public ReadingService(
            IContextFactory contextFactory,
            IInputFileRepository inputFileRepository,
            IReadingsImportService readingsImportService,
            IReadingRepository readingRepository)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _inputFileRepository = inputFileRepository ?? throw new ArgumentNullException(nameof(inputFileRepository));
            this._readingsImportService = readingsImportService ?? throw new ArgumentNullException(nameof(readingsImportService));
            this._readingRepository = readingRepository ?? throw new ArgumentNullException(nameof(readingRepository));
        }
        public void CreateReadingsFromInputFile(InputFileGetDto inputFile)
        {
            if (inputFile is null)
            {
                throw new ArgumentNullException(nameof(inputFile));
            }



            using var stream = new MemoryStream(inputFile.Bytes);

            var importResult = _readingsImportService.Import(stream);



            var readingInsertDtos = importResult.Readings
                .Select(x => new ReadingInsertDto(
                        inputFile.InputFileId, Status.New, x.SensorId, x.SensorTypeName, x.Unit, x.StationId
                        , x.StationName, x.Value, x.Province, x.City, x.IsHystoric, x.StartDate, x.StopDate
                        , x.UtmNord, x.UtmEst, x.Latitude, x.Longitude))
                .ToList();

            Status status = importResult.Success ? Status.Success : Status.Error;
            var inputFileUpdateDto = new InputFileUpdateDto(inputFile.InputFileId, status, importResult.Errors);

            using IContext context = _contextFactory.Create();
            _inputFileRepository.Update(inputFileUpdateDto, context);

            readingInsertDtos.ForEach(readingDto =>
                _readingRepository.Insert(readingDto, context));

            context.Commit();





        }
        public List<ReadingGetDto> GetReadingsFromDb()
        {
            using (IContext context = _contextFactory.Create())
            {
                return _readingRepository.GetByStatus(Status.New, context).ToList();
            }
        }
    }
}
