using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;
using Training.Data;

namespace Training.DataTests
{
    public static  class AssertHelper
    {
        public static void AreEqual(ReadingInsertDto expected, dynamic retrievedItem)
        {
            Assert.AreEqual(expected.InputFileId, retrievedItem.InputFileId);
            Assert.AreEqual(expected.SensorId, retrievedItem.SensorId);
            Assert.AreEqual(expected.SensorTypeName, retrievedItem.SensorTypeName);
            Assert.AreEqual(expected.Unit, retrievedItem.Unit);
            Assert.AreEqual(expected.StationId, retrievedItem.StationId);
            Assert.AreEqual(expected.StationName, retrievedItem.StationName);
            Assert.AreEqual(expected.Value, retrievedItem.Value);
            Assert.AreEqual(expected.Province, retrievedItem.Province);
            Assert.AreEqual(expected.City, retrievedItem.City);
            Assert.AreEqual(expected.IsHystoric, retrievedItem.IsHystoric);
            Assert.AreEqual(expected.StartDate, retrievedItem.StartDate);
            Assert.AreEqual(expected.StopDate, retrievedItem.StopDate);
            Assert.AreEqual(expected.UtmNord, retrievedItem.UtmNord);
            Assert.AreEqual(expected.UtmEst, retrievedItem.UtmEst);
            Assert.AreEqual(expected.Latitude, retrievedItem.Latitude);
            Assert.AreEqual(expected.Longitude, retrievedItem.Longitude);
        }

     
    }
}
