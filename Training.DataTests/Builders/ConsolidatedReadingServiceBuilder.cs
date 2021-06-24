using ConsoleWithDb.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Data;

namespace Training.DataTests.Builders
{
    public class ConsolidatedReadingServiceBuilder
    {
        private IConsolidatedReadingRepository _consolidatedReadingRepository;
        private IContextFactory _contextFactory;
        private IConsistentReadingFactory _consistentReadingFactory;
        private IReadingRepository _readingRepository;

        public ConsolidatedReadingService Build()
        {
            return new ConsolidatedReadingService(_consolidatedReadingRepository, _contextFactory, _consistentReadingFactory, _readingRepository);
        }

        public ConsolidatedReadingServiceBuilder WithRealDependencies()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var connectionString = configuration.GetConnectionString("Default");
            _contextFactory = new DapperContextFactory(connectionString);


            _readingRepository = new ReadingRepository();
            _consolidatedReadingRepository = new ConsolidatedReadingRepository();
            _consistentReadingFactory = new ConsistentReadingFactory(new ReadingValidator());
            return this;
        }
    }
}
