using System;
using System.Collections.Generic;

namespace Training.Core.ProvinceData
{
    public class ProvinceData
    {
        public ProvinceData(string province, string sensorTypeName, IList<ConsistentReading> consistentReadings)
        {
            Province = province ?? throw new ArgumentNullException(nameof(province));
            ConsistentReadings = consistentReadings ?? throw new ArgumentNullException(nameof(consistentReadings));
            SensorTypeName = sensorTypeName;
        }
        public string Province { get; }
        public string SensorTypeName { get; }
        public IList<ConsistentReading> ConsistentReadings { get; }
    }
}
