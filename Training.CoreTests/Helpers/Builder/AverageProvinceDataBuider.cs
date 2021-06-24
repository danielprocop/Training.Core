using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;

namespace Training.CoreTests.Helpers.Builder
{
    public class AverageProvinceDataBuider
    {
        private string _province;
        private string _sensorTypeName;
        private int _averageValue;
        private Unit _unit;
        private int _averageDaysOfMeasure;

        public AverageProvinceDataBuider SetDefaults()
        {
            _province = "prov";
            _sensorTypeName = "sens01";
            _averageValue = 1;
            _unit = Unit.ng_m3;
            _averageDaysOfMeasure = 1;
            return this;
        }
        public AverageProvinceDataBuider SetProvince(string province)
        {
            _province = province;
            return this;
        }
        public AverageProvinceDataBuider SetSensorTypeName(string sensorTypeName)
        {
            _sensorTypeName = sensorTypeName;
            return this;
        }

        public AverageProvinceDataBuider SetAverageValue(int averageValue)
        {
            _averageValue = averageValue;
            return this;
        }
        public AverageProvinceDataBuider SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }
        public AverageProvinceDataBuider SetAverageDaysOfMeasure(int averageDaysOfMeasure)
        {
            _averageDaysOfMeasure = averageDaysOfMeasure;
            return this;
        }
        public AverageProvinceData Build()
        {
            return new AverageProvinceData(_province, _sensorTypeName,_averageValue,_unit,_averageDaysOfMeasure);
        }
    }
}
