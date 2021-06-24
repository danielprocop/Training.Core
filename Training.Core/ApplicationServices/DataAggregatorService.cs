using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;

namespace App.Servicess
{
    public class DataAggregatorService
    {
        private readonly IProvinceDataListFactory _provinceDataListFactory;
        private readonly IAverageProvinceDataFactory _averageProvinceDataFactory;
        private readonly IAverageProvinceDataExportService _averageProvinceDataExportService;
        private readonly LogService _logService;

        public DataAggregatorService(IProvinceDataListFactory provinceDataListFactory
            , IAverageProvinceDataFactory averageProvinceDataFactory
            , IAverageProvinceDataExportService averageProvinceDataExportService
            , LogService logService
            )
        {
            this._provinceDataListFactory = provinceDataListFactory;
            this._averageProvinceDataFactory = averageProvinceDataFactory;
            this._averageProvinceDataExportService = averageProvinceDataExportService;
            this._logService = logService;
        }
        public void CalculateAverageAndWriteToFile(IList<ConsistentReading> consistentReadings, Stream outputStream)
        {
            if (consistentReadings == null)
            {
                throw new ArgumentNullException(nameof(consistentReadings));
            }
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            var provinceDataList = _provinceDataListFactory.CreateProvinceDataList(consistentReadings);


            List<Result<AverageProvinceData>> averageProvinceDataResults = new List<Result<AverageProvinceData>>();
            provinceDataList.ToList().ForEach(provinceData =>
                    averageProvinceDataResults.Add(_averageProvinceDataFactory.CreateProvinceAverageData(provinceData)));

            _logService.LogErrors(averageProvinceDataResults);
            
            var averageProvinceDataList = averageProvinceDataResults
                .Where(x=>x.Success)
                .Select(x => x.Value)
                .ToList();

            if (averageProvinceDataList.Count > 0)
            {
                _averageProvinceDataExportService.Export(averageProvinceDataList, outputStream);
            }
          
        }

       
    }
}
