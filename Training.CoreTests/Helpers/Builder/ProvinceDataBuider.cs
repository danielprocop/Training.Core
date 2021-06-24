using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;

namespace Training.CoreTests
{
    public class ProvinceDataBuider
    {
        private string _province;
        private string _sensorTypeName;
        private IList<ConsistentReading> _consistentReadings;

        public ProvinceDataBuider SetProvince(string province)
        {
            _province = province;
            return this;
        }
        public ProvinceDataBuider SetSensorTypeName(string sensorTypeName)
        {
            _sensorTypeName = sensorTypeName;
            return this;
        }

        public ProvinceDataBuider SetConsistentReadings(IList<ConsistentReading> consistentReadings)
        {
            _consistentReadings = consistentReadings;
            return this;
        }

        public ProvinceData Build()
        {
            return new ProvinceData(_province, _sensorTypeName, _consistentReadings);
        }
    }
}
