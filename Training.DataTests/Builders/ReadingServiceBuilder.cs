using ConsoleWithDb.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Data;

namespace Training.DataTests.Builders
{
    public class ReadingServiceBuilder
    {
        private IContextFactory _contextFactory;
        private IInputFileRepository _inputFileRepository;
        private IReadingsImportService _readingsImportService;
        private IReadingRepository _readingRepository;

        public ReadingService Build()
        {
            return new ReadingService(_contextFactory, _inputFileRepository, _readingsImportService, _readingRepository);
        }

        public ReadingServiceBuilder WithRealDependencies()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var connectionString = configuration.GetConnectionString("Default");
            _contextFactory = new DapperContextFactory(connectionString);

            _inputFileRepository = new InputFileRepository();
            _readingRepository = new ReadingRepository();
            _readingsImportService = new ReadingsImportService();
            return this;
        }
    }
}
