using App.Servicess;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;

namespace Training.CoreTests.Helpers.Builder
{
    public class DataAggregatorServiceBuilder
    {
        IProvinceDataListFactory provinceDataListFactory;
        IAverageProvinceDataFactory averageProvinceDataFactory;
        IAverageProvinceDataExportService averageProvinceDataExportService;
        LogService logService;

        public DataAggregatorService Build()
        {
            return new DataAggregatorService(provinceDataListFactory, averageProvinceDataFactory, averageProvinceDataExportService, logService);
        }

        public DataAggregatorServiceBuilder()
        {
            provinceDataListFactory = new Mock<IProvinceDataListFactory>().Object;
            averageProvinceDataFactory = new Mock<IAverageProvinceDataFactory>().Object;
            averageProvinceDataExportService = new Mock<IAverageProvinceDataExportService>().Object;
            logService = new Mock<LogService>(new Mock<ILogger<LogService>>().Object).Object;
        }

        public DataAggregatorServiceBuilder WithProvinceDataListFactory(IProvinceDataListFactory provinceDataListFactory)
        {
            this.provinceDataListFactory = provinceDataListFactory;
            return this;
        }
        public DataAggregatorServiceBuilder WithAverageProvinceDataFactory(IAverageProvinceDataFactory averageProvinceDataFactory)
        {
            this.averageProvinceDataFactory = averageProvinceDataFactory;
            return this;
        }
        public DataAggregatorServiceBuilder WithAverageProvinceDataExportService(IAverageProvinceDataExportService averageProvinceDataExportService)
        {
            this.averageProvinceDataExportService = averageProvinceDataExportService;
            return this;
        }
        public DataAggregatorServiceBuilder WithLogService(LogService logService)
        {
            this.logService = logService;
            return this;
        }

        public DataAggregatorServiceBuilder WithRealServices()
        {
            provinceDataListFactory = new ProvinceDataListFactory();
            averageProvinceDataFactory = new AverageProvinceDataFactory();
            averageProvinceDataExportService = new AverageProvinceDataExportService();
            var logger = new Mock<ILogger<LogService>>();
            logService = new LogService(logger.Object);

            return this;
        }
    }
}
