using System;
using System.Collections.Generic;
using System.Text;
using Training.Data;

namespace Training.DataTests.Builders
{
    public class ReadingInsertDtoBuilder
    {
        private long _inputFileId;
        private Status _status;
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

      

        public ReadingInsertDtoBuilder()
        {
            _inputFileId = 1;
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
            _status = Status.New;
        }

        public ReadingInsertDto Build()
        {
            return new ReadingInsertDto(
                _inputFileId
                , _status
                , _sensorId
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

        public ReadingInsertDtoBuilder SetInputFileId(long inputFileId)
        {
            _inputFileId = inputFileId;
            return this;
        }
        public ReadingInsertDtoBuilder SetStatus(Status status)
        {
            _status = status;
            return this;
        }
        public ReadingInsertDtoBuilder SetSensorId(long sensorId)
        {
            _sensorId = sensorId;
            return this;
        }
        public ReadingInsertDtoBuilder SetSensorTypeName(string sensorTypeName)
        {
            _sensorTypeName = sensorTypeName;
            return this;
        }
        public ReadingInsertDtoBuilder SetUnit(string unit)
        {
            _unit = unit;
            return this;
        }
        public ReadingInsertDtoBuilder SetStationId(long stationId)
        {
            _stationId = stationId;
            return this;
        }
        public ReadingInsertDtoBuilder SetStationName(string stationName)
        {
            _stationName = stationName;
            return this;
        }
        public ReadingInsertDtoBuilder SetValue(int value)
        {
            _value = value;
            return this;
        }
        public ReadingInsertDtoBuilder SetProvince(string province)
        {
            _province = province;
            return this;
        }
        public ReadingInsertDtoBuilder SetCity(string city)
        {
            _city = city;
            return this;
        }
        public ReadingInsertDtoBuilder SetIsHystoric(bool isHystoric)
        {
            _isHystoric = isHystoric;
            return this;
        }
        public ReadingInsertDtoBuilder SetStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }
        public ReadingInsertDtoBuilder SetStopDate(DateTime? stopDate)
        {
            _stopDate = stopDate;
            return this;
        }
        public ReadingInsertDtoBuilder SetUtmNord(int utmNord)
        {
            _utmNord = utmNord;
            return this;
        }
        public ReadingInsertDtoBuilder SetUtmEst(int utmEst)
        {
            _utmEst = utmEst;
            return this;
        }
        public ReadingInsertDtoBuilder SetLatitude(string latitude)
        {
            _latitude = latitude;
            return this;
        }
        public ReadingInsertDtoBuilder SetLongitude(string longitude)
        {
            _longitude = longitude;
            return this;
        }
    }
}
