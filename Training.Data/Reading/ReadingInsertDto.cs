using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Data
{
    public class ReadingInsertDto
    {
        public ReadingInsertDto(long inputFileId, Status status, long sensorId, string sensorTypeName, string unit, long stationId,
            string stationName, int value, string province, string city, bool isHystoric,
            DateTime startDate, DateTime? stopDate, int utmNord, int utmEst,
            string latitude, string longitude)
        {
            InputFileId = inputFileId;
            Status = status;
            SensorId = sensorId;
            SensorTypeName = sensorTypeName;
            Unit = unit;
            StationId = stationId;
            StationName = stationName;
            Value = value;
            Province = province;
            City = city;
            IsHystoric = isHystoric;
            StartDate = startDate;
            StopDate = stopDate;
            UtmNord = utmNord;
            UtmEst = utmEst;
            Latitude = latitude;
            Longitude = longitude;
         
        }
        public long InputFileId { get; }
        public Status Status { get;}
        public long SensorId { get; }
        public string SensorTypeName { get; }
        public string Unit { get; }
        public long StationId { get; }
        public string StationName { get; }
        public int Value { get; }
        public string Province { get; }
        public string City { get; }
        public bool IsHystoric { get; }

        public DateTime StartDate { get; }

        public DateTime? StopDate { get; }
        public int UtmNord { get; }
        public int UtmEst { get; }

        public string Latitude { get; }

        public string Longitude { get; }
    }
}
