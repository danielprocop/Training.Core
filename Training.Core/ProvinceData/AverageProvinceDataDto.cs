using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Core.ProvinceData
{
    [DelimitedRecord(",")]
    public class AverageProvinceDataDto
    {

        public string Province { get; set; }
        public string SensorTypeName { get; set; }
        public int AverageValue { get; set; }
        public string Unit { get; set; }
        public int AverageDaysOfMeasure { get; set; }
    }
}
