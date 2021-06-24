using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.Core.ProvinceData
{
    public class ProvinceDataListFactory : IProvinceDataListFactory
    {
        public IList<ProvinceData> CreateProvinceDataList(IList<ConsistentReading> consistentReadings)
        {
            if (consistentReadings == null)
            {
                throw new ArgumentNullException(nameof(consistentReadings));
            }

            var list=consistentReadings.GroupBy(
                p => new { p.Province, p.SensorTypeName },
                p => p,
                (key, g) => new ProvinceData(
                    province : key.Province
                    , sensorTypeName : key.SensorTypeName
                    , consistentReadings : g.ToList()))
                .ToList();

            return list;
        }
    }
}
