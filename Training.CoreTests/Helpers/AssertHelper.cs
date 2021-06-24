using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;

namespace Training.CoreTests
{
    public static  class AssertHelper
    {
        public static void AreEqual(AverageProvinceData expected, AverageProvinceData actual)
        {
            Assert.AreEqual(expected.Province, actual.Province);
            Assert.AreEqual(expected.SensorTypeName, actual.SensorTypeName);
            Assert.AreEqual(expected.AverageValue, actual.AverageValue);
            Assert.AreEqual(expected.AverageDaysOfMeasure, actual.AverageDaysOfMeasure);
            Assert.AreEqual(expected.Unit, actual.Unit);
        }

        public static void AreEqual(ConsistentReading expected, ConsistentReading actual)
        {
            Assert.AreEqual(expected.SensorId, actual.SensorId);
            Assert.AreEqual(expected.SensorTypeName, actual.SensorTypeName);
            Assert.AreEqual(expected.Unit, actual.Unit);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.Province, actual.Province);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.IsHystoric, actual.IsHystoric);
            Assert.AreEqual(expected.DaysOfMeasure, actual.DaysOfMeasure);
            Assert.AreEqual(expected.UtmNord, actual.UtmNord);
            Assert.AreEqual(expected.UtmEst, actual.UtmEst);
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Longitude, actual.Longitude);
        }

        public static void AreEqual(Reading expected, Reading actual)
        {
            Assert.AreEqual(expected.SensorId, actual.SensorId);
            Assert.AreEqual(expected.SensorTypeName, actual.SensorTypeName);
            Assert.AreEqual(expected.Unit, actual.Unit);
            Assert.AreEqual(expected.StationId, actual.StationId);
            Assert.AreEqual(expected.StationName, actual.StationName);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.Province, actual.Province);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.IsHystoric, actual.IsHystoric);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.StopDate, actual.StopDate);
            Assert.AreEqual(expected.UtmNord, actual.UtmNord);
            Assert.AreEqual(expected.UtmEst, actual.UtmEst);
            Assert.AreEqual(expected.Latitude, actual.Latitude);
            Assert.AreEqual(expected.Longitude, actual.Longitude);
        }
    }
}
