using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemWrapper.Interface;
using Training.Core;

namespace ConsoleWithDb.Services
{
    public class SystemManager
    {
        private readonly IInputFileService _inputFileService;
        private readonly IReadingService _readingService;
        private readonly IConsolidatedReadingService _consolidatedReadingService;
        private readonly IOutputFileService _outputFileService;

        public SystemManager(IInputFileService inputFileService,
            IReadingService readingService,
            IConsolidatedReadingService consolidatedReadingService,
            IOutputFileService outputFileService)
        {
            if (inputFileService is null)
            {
                throw new ArgumentNullException(nameof(inputFileService));
            }

            if (readingService is null)
            {
                throw new ArgumentNullException(nameof(readingService));
            }

            if (consolidatedReadingService is null)
            {
                throw new ArgumentNullException(nameof(consolidatedReadingService));
            }

            if (outputFileService is null)
            {
                throw new ArgumentNullException(nameof(outputFileService));
            }

            _inputFileService = inputFileService;
            this._readingService = readingService;
            this._consolidatedReadingService = consolidatedReadingService;
            this._outputFileService = outputFileService;
        }
        public void Run()
        {
            var files = _inputFileService.GetFilesFromInputPath();
            files.ForEach(fileInfo=>
                _inputFileService.CreateInputFile(fileInfo));

            var inputFiles = _inputFileService.GetInputFilesFromDb();
            inputFiles.ForEach(inputFile => 
                _readingService.CreateReadingsFromInputFile(inputFile));

            var readings = _readingService.GetReadingsFromDb();
            readings.ForEach(reading => 
                _consolidatedReadingService.CreateConsolidatedReading(reading));

            var consolidatedReadings = _consolidatedReadingService.GetConsolidatedReadingsFromDb();
            _outputFileService.CreateOutputFileFromConsistentReadings(consolidatedReadings);
        }

      
    }
}
