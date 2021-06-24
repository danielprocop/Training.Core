using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;

namespace Training.CoreTests
{
    public class ReadingBuilder
    {
        private long _sensorId;
        private string _sensorTypeName;
        private string _unit;
        private long _stationId;
        private string _stationName;
        private int _value;
        private string _province;
        private string _city;
        private bool _isHystoric;
        private DateTime _startDate = new DateTime(1, 1, 1);
        private DateTime? _stopDate = new DateTime(1, 1, 1);
        private int _utmNord;
        private int _utmEst;
        private string _latitude;
        private string _longitude;

        public ReadingBuilder SetDefaults()
        {
            _sensorId = 1;
            _sensorTypeName = "sensorTypeName";
            _unit = "ng_m3";
            _stationId = 1;
            _stationName = "stationName";
            _value = 1;
            _province = "province";
            _city = "city";
            _isHystoric = true;
            _startDate = new DateTime(2000, 1, 1);
            _stopDate = null;
            _utmNord = 1;
            _utmEst = 1;
            _latitude = "0";
            _longitude = "0";
            return this;
        }

        public Reading Build()
        {
            return new Reading(
                _sensorId
                , _sensorTypeName
                , _unit
                , _stationId
                , _stationName
                , _value
                , _province
                , _city
                , _isHystoric
                , _startDate
                , _stopDate
                , _utmNord
                , _utmEst
                , _latitude
                , _longitude);
        }
        public ReadingBuilder SetSensorId(long sensorId)
        {
            _sensorId = sensorId;
            return this;
        }
        public ReadingBuilder SetSensorTypeName(string sensorTypeName)
        {
            _sensorTypeName = sensorTypeName;
            return this;
        }
        public ReadingBuilder SetUnit(string unit)
        {
            _unit = unit;
            return this;
        }
        public ReadingBuilder SetStationId(long stationId)
        {
            _stationId = stationId;
            return this;
        }
        public ReadingBuilder SetStationName(string stationName)
        {
            _stationName = stationName;
            return this;
        }
        public ReadingBuilder SetValue(int value)
        {
            _value = value;
            return this;
        }
        public ReadingBuilder SetProvince(string province)
        {
            _province = province;
            return this;
        }
        public ReadingBuilder SetCity(string city)
        {
            _city = city;
            return this;
        }
        public ReadingBuilder SetIsHystoric(bool isHystoric)
        {
            _isHystoric = isHystoric;
            return this;
        }
        public ReadingBuilder SetStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }
        public ReadingBuilder SetStopDate(DateTime? stopDate)
        {
            _stopDate = stopDate;
            return this;
        }
        public ReadingBuilder SetUtmNord(int utmNord)
        {
            _utmNord = utmNord;
            return this;
        }
        public ReadingBuilder SetUtmEst(int utmEst)
        {
            _utmEst = utmEst;
            return this;
        }
        public ReadingBuilder SetLatitude(string latitude)
        {
            _latitude = latitude;
            return this;
        }
        public ReadingBuilder SetLongitude(string longitude)
        {
            _longitude = longitude;
            return this;
        }
    }
}
