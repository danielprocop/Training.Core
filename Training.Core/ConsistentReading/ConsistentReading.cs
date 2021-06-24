namespace Training.Core
{
    public class ConsistentReading
    {
        public ConsistentReading(long sensorId, string sensorTypeName, Unit unit, int value, string province, string city, bool isHystoric, int daysOfMeasure, int utmNord, int utmEst, string latitude, string longitude)
        {
            SensorId = sensorId;
            SensorTypeName = sensorTypeName;
            Unit = unit;
            Value = value;
            Province = province;
            City = city;
            IsHystoric = isHystoric;
            DaysOfMeasure = daysOfMeasure;
            UtmNord = utmNord;
            UtmEst = utmEst;
            Latitude = latitude;
            Longitude = longitude;
        }

        public long SensorId { get; }
        public string SensorTypeName { get; }
        public Unit Unit { get; }
        public int Value { get; }
        public string Province { get; }
        public string City { get; }
        public bool IsHystoric { get; }
        public int DaysOfMeasure { get; }
        public int UtmNord { get; }
        public int UtmEst { get; }
        public string Latitude { get; }
        public string Longitude { get; }
    }

}
