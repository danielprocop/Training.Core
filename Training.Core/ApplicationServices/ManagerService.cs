using App.Servicess;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;

namespace Training.Core.ApplicationServices
{
    public class ManagerService
    {
        private readonly ILogger<ManagerService> _logger;
        private readonly DataAggregatorService _dataAggregatorService;
        private readonly ReaderFileService _readerFileService;
        private readonly string _pathOutput;
        private readonly string _pathInput;

        public ManagerService(
            ILogger<ManagerService> logger
            , DataAggregatorService dataAggregatorService
            , ReaderFileService readerFileService
            , IOptions<Paths> paths)
        {
            _logger = logger;
            this._dataAggregatorService = dataAggregatorService;
            this._readerFileService = readerFileService;
            this._pathOutput = paths.Value.OutputFile;
            this._pathInput = paths.Value.InputPath;

            _logger.LogInformation("App starting ...");
        }
        public void Run()
        {

            DirectoryInfo dir = new DirectoryInfo(_pathInput);

            List<ConsistentReading> consistentReadings = new List<ConsistentReading>();
            foreach (FileInfo fileInfo in dir.GetFiles())
            {
                using (FileStream inputStream = File.OpenRead(fileInfo.FullName))
                {

                    consistentReadings.AddRange(_readerFileService.ReadFile(inputStream));
                }
            }

            if (File.Exists(_pathOutput))
            {
                File.Delete(_pathOutput);
            }

            using (var outputStream = File.Open(_pathOutput, FileMode.CreateNew))
            {
                _dataAggregatorService.CalculateAverageAndWriteToFile(consistentReadings, outputStream);
            }


            _logger.LogInformation("File output created");
        }
    }
}
