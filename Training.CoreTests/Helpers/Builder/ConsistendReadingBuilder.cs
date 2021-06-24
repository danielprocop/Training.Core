using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;

namespace Training.CoreTests
{
    public class ConsistendReadingBuilder
    {
        protected long _sensorId;
        protected string _sensorTypeName;
        protected Unit _unit;
        protected int _value;
        protected string _province;
        protected string _city;
        protected bool _isHystoric;
        protected int _daysOfMeasure;
        protected int _utmNord;
        protected int _utmEst;
        protected string _latitude;
        protected string _longitude;

        public ConsistentReading Build()
        {
            return new ConsistentReading(_sensorId, _sensorTypeName, _unit, _value, _province, _city, _isHystoric, _daysOfMeasure, _utmNord, _utmEst, _latitude, _longitude);
        }
        public ConsistendReadingBuilder SetSensorId(long sensorId)
        {
            _sensorId = sensorId;
            return this;
        }
        public ConsistendReadingBuilder SetSensorTypeName(string sensorTypeName)
        {
            _sensorTypeName = sensorTypeName;
            return this;
        }
        public ConsistendReadingBuilder SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }
        public ConsistendReadingBuilder SetValue(int value)
        {
            _value = value;
            return this;
        }
        public ConsistendReadingBuilder SetProvince(string province)
        {
            _province = province;
            return this;
        }
        public ConsistendReadingBuilder SetCity(string city)
        {
            _city = city;
            return this;
        }
        public ConsistendReadingBuilder SetIsHystoric(bool isHystoric)
        {
            _isHystoric = isHystoric;
            return this;
        }
        public ConsistendReadingBuilder SetDaysOfMeasuree(int daysOfMeasure)
        {
            _daysOfMeasure = daysOfMeasure;
            return this;
        }
      
        public ConsistendReadingBuilder SetUtmNord(int utmNord)
        {
            _utmNord = utmNord;
            return this;
        }
        public ConsistendReadingBuilder SetUtmEst(int utmEst)
        {
            _utmEst = utmEst;
            return this;
        }
        public ConsistendReadingBuilder SetLatitude(string latitude)
        {
            _latitude = latitude;
            return this;
        }
        public ConsistendReadingBuilder SetLongitude(string longitude)
        {
            _longitude = longitude;
            return this;
        }
        public ConsistendReadingBuilder SetDataFromReading(Reading reading)
        {
            _sensorId = reading.SensorId;
            _sensorTypeName = reading.SensorTypeName;
            _unit = (Unit)Enum.Parse(typeof(Unit),reading.Unit);
            _value = reading.Value;
            _province = reading.Province;
            _city = reading.City;
            _isHystoric = reading.IsHystoric;
            _daysOfMeasure = ExtractDayOfMeasure(reading.StartDate, reading.StopDate);
            _utmNord = reading.UtmNord;
            _utmEst = reading.UtmEst;
            _latitude = reading.Latitude;
            _longitude = reading.Longitude;
            return this;
        }

        private int ExtractDayOfMeasure(DateTime startDate, DateTime? stopDate)
        {
            int daysOfMeasure;
            if (stopDate != null)
            {
                daysOfMeasure = (startDate - (DateTime)stopDate).Days;
            }
            else
            {
                daysOfMeasure = DateTime.Now.Day;
            }

            return daysOfMeasure;
        }

        public ConsistendReadingBuilder SetDefaults()
        {
            _sensorId = 1;
            _sensorTypeName = "sensorTypeName";
            _unit = Unit.ng_m3;
            _value = 1;
            _province = "province";
            _city = "city";
            _isHystoric = true;
            _daysOfMeasure = 1;
            _utmNord = 1;
            _utmEst = 1;
            _latitude = "0";
            _longitude = "0";
            return this;
        }
    }
}
