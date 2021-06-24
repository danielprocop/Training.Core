using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Core;
using Training.Data;
using Training.Data.Reading;

namespace ConsoleWithDb.Services
{
    public class ConsolidatedReadingService : IConsolidatedReadingService
    {
        private readonly IConsolidatedReadingRepository _consolidatedReadingRepository;
        private readonly IContextFactory _contextFactory;
        private readonly IConsistentReadingFactory _consistentReadingFactory;
        private readonly IReadingRepository _readingRepository;

        public ConsolidatedReadingService(IConsolidatedReadingRepository consolidatedReadingRepository,
            IContextFactory contextFactory,
            IConsistentReadingFactory consistentReadingFactory,
            IReadingRepository readingRepository)
        {
            if (consolidatedReadingRepository is null)
            {
                throw new ArgumentNullException(nameof(consolidatedReadingRepository));
            }

            if (contextFactory is null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            if (consistentReadingFactory is null)
            {
                throw new ArgumentNullException(nameof(consistentReadingFactory));
            }

            if (readingRepository is null)
            {
                throw new ArgumentNullException(nameof(readingRepository));
            }

            this._consolidatedReadingRepository = consolidatedReadingRepository;
            this._contextFactory = contextFactory;
            this._consistentReadingFactory = consistentReadingFactory;
            this._readingRepository = readingRepository;
        }
        public void CreateConsolidatedReading(ReadingGetDto readingDto)
        {
            if (readingDto is null)
            {
                throw new ArgumentNullException(nameof(readingDto));
            }
            using (IContext context = _contextFactory.Create())
            {
                Reading reading = new Reading(readingDto.SensorId, readingDto.SensorTypeName, readingDto.Unit, readingDto.StationId, readingDto.StationName, readingDto.Value, readingDto.Province, readingDto.City, readingDto.IsHystoric, readingDto.StartDate, readingDto.StopDate, readingDto.UtmNord, readingDto.UtmEst, readingDto.Latitude, readingDto.Longitude);
                var result = _consistentReadingFactory.CreateConsistentReading(reading);
                if (result.Success)
                {
                    var consolidatedReadingDto = new ConsolidatedReadingInsertDto(readingDto.Id, Status.New, result.Value.SensorId, result.Value.SensorTypeName, result.Value.Unit, result.Value.Value, result.Value.Province, result.Value.City, result.Value.IsHystoric, result.Value.DaysOfMeasure, result.Value.UtmNord, result.Value.UtmEst, result.Value.Latitude, result.Value.Longitude);
                    _consolidatedReadingRepository.Insert(consolidatedReadingDto, context);
                }
                Status status = result.Success ? Status.Success : Status.Error;
                ReadingUpdateDto readingUpdateDto = new ReadingUpdateDto(readingDto.Id, status, result.Errors);
                _readingRepository.Update(readingUpdateDto, context);

                context.Commit();

            }
        }

        public List<ConsolidatedReadingGetDto> GetConsolidatedReadingsFromDb()
        {
            using (IContext context = _contextFactory.Create())
            {
                return _consolidatedReadingRepository.GetByStatus(Status.New, context).ToList();
            }
        }
    }
}
