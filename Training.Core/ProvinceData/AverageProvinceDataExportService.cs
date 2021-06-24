using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Training.Core.ProvinceData
{
    public class AverageProvinceDataExportService : IAverageProvinceDataExportService
    {
        public void Export(IList<AverageProvinceData> averageProvinceDataList, Stream outputStream)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            if (averageProvinceDataList == null)
            {
                throw new ArgumentNullException(nameof(averageProvinceDataList));
            }
            var data=averageProvinceDataList.Select(x => new AverageProvinceDataDto()
            {
                Province = x.Province,
                SensorTypeName = x.SensorTypeName,
                Unit = x.Unit.ToString(),
                AverageValue = x.AverageValue,
                AverageDaysOfMeasure = x.AverageDaysOfMeasure
            }).ToList();

            var engine = new FileHelperEngine<AverageProvinceDataDto>();
            using (var streamWriter = new StreamWriter(outputStream))
            {
                engine.WriteStream(streamWriter, data);
            }

        }

     
    }
}
