namespace Training.Core.ProvinceData
{
    public class AverageProvinceData
    {
        public AverageProvinceData(string province, string sensorTypeName, int averageValue, Unit unit, int averageDaysOfMeasure)
        {
            Province = province;
            SensorTypeName = sensorTypeName;
            AverageValue = averageValue;
            Unit = unit;
            AverageDaysOfMeasure = averageDaysOfMeasure;
        }
        public string Province { get; }
        public string SensorTypeName { get; }
        public int AverageValue { get; }
        public Unit Unit { get; }
        public int AverageDaysOfMeasure { get; }
    }
}
