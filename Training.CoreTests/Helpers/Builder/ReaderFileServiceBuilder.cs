using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;

namespace Training.CoreTests.Helpers.Builder
{
    public class ReaderFileServiceBuilder
    {
        IReadingsImportService readingsImportService;
        IConsistentReadingFactory consistentReadingFactory;
        LogService logService;

        public ReaderFileService Build()
        {
            return new ReaderFileService(readingsImportService, consistentReadingFactory, logService);
        }

        public ReaderFileServiceBuilder()
        {
            readingsImportService = new Mock<IReadingsImportService>().Object;
            consistentReadingFactory = new Mock<IConsistentReadingFactory>().Object;
            logService = new Mock<LogService>(new Mock<ILogger<LogService>>().Object).Object;
        }

        public ReaderFileServiceBuilder WithReadingsImportService(IReadingsImportService readingsImportService)
        {
            this.readingsImportService = readingsImportService;
            return this;
        }
        public ReaderFileServiceBuilder WithConsistentReadingFactory(IConsistentReadingFactory consistentReadingFactory)
        {
            this.consistentReadingFactory = consistentReadingFactory;
            return this;
        }
        public ReaderFileServiceBuilder WithLogService(LogService logService)
        {
            this.logService = logService;
            return this;
        }

        public ReaderFileServiceBuilder WithRealServices()
        {
            readingsImportService = new ReadingsImportService();
            consistentReadingFactory = new ConsistentReadingFactory(new ReadingValidator());
            var logger = new Mock<ILogger<LogService>>();
            logService = new LogService(logger.Object);

            return this;
        }
    }
}
