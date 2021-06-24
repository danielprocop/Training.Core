using ConsoleWithDb.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Data;

namespace Training.DataTests.Builders
{
    public class OutputFileServiceBuilder
    {
        private IOutputFileRepository _outputFileRepository;
        private IContextFactory _contextFactory;
        private IConsolidatedReadingRepository _consolidatedReadingRepository;

        public OutputFileService Build()
        {
            return new OutputFileService(_outputFileRepository, _contextFactory, _consolidatedReadingRepository);
        }
        public OutputFileServiceBuilder WithRealDependencies()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var connectionString = configuration.GetConnectionString("Default");
            _contextFactory = new DapperContextFactory(connectionString);

            _outputFileRepository = new OutputFileRepository();
            _consolidatedReadingRepository = new ConsolidatedReadingRepository();
            return this;
        }
    }
}
