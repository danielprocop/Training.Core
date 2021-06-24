using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Training.Core.ApplicationServices
{
    public class ReaderFileService
    {
        private readonly IReadingsImportService _readingsImportService;
        private readonly IConsistentReadingFactory _consistentReadingFactory;
        private readonly LogService _logService;

        public ReaderFileService(IReadingsImportService readingsImportService
          , IConsistentReadingFactory consistentReadingFactory, LogService logService)
        {
            this._readingsImportService = readingsImportService;
            this._consistentReadingFactory = consistentReadingFactory;
            this._logService = logService;
        }

        public IList<ConsistentReading> ReadFile(Stream inputStream)
        {
            ImportResult importResult = _readingsImportService.Import(inputStream);
            _logService.LogErrors(importResult.Errors);
            return FindConsistentReadings(importResult.Readings);
 
        }
        private IList<ConsistentReading> FindConsistentReadings(IList<Reading> readings)
        {
            var consistentReadingResults = new List<Result<ConsistentReading>>();
            readings.ToList().ForEach(reading => 
                consistentReadingResults.Add(_consistentReadingFactory.CreateConsistentReading(reading)));

            _logService.LogErrors(consistentReadingResults);

            return consistentReadingResults
                .Where(x => x.Success == true)
                .Select(x=>x.Value)
                .ToList();
        }

     

    }
}
