using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Training.Core.ProvinceData
{
    public class AverageProvinceDataFactory : IAverageProvinceDataFactory
    {
        public Result<AverageProvinceData> CreateProvinceAverageData(ProvinceData provinceData)
        {
            if (provinceData == null)
            {
                throw new ArgumentNullException(nameof(provinceData));
            }
            if (provinceData.ConsistentReadings.Count == 0)
            {
                throw new ArgumentException("the provinceData must have al least one record of consistent reading");
            }
            if (provinceData.ConsistentReadings.GroupBy(x => x.Unit).Count()!=1)
            {
                return Result<AverageProvinceData>.Ko(new List<string>() {"Error: it does not make sense make average calculations if the units of measure of the items are different from one another" });
            }

            var averageValue = (int)provinceData.ConsistentReadings.Average(x => x.Value);
            var averageDaysOfMeasure = (int)provinceData.ConsistentReadings.Average(x => x.DaysOfMeasure);
            var averageProvinceData = new AverageProvinceData(
                provinceData.Province
                , provinceData.SensorTypeName
                , averageValue
                , provinceData.ConsistentReadings[0].Unit
                , averageDaysOfMeasure);
               

            return Result<AverageProvinceData>.Ok(averageProvinceData);


        }
    }
}
