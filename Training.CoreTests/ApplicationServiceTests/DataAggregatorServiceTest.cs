using App.Servicess;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;
using Training.CoreTests.Helpers.Builder;

namespace Training.CoreTests.ApplicationServiceTests
{
    [TestClass]
    public class DataAggregatorServiceTest
    {

        [TestMethod]
        public void CalculateAverageAndWriteToFile_ListOfRightConsistentReandings_ReturnOneLineOfAverageReading()
        {
            DataAggregatorService sut = new DataAggregatorServiceBuilder()
                .WithRealServices()
                .Build(); 

            IList<ConsistentReading> consistentReadings = new List<ConsistentReading>()
           {
               new ConsistendReadingBuilder().SetDefaults().Build(),
               new ConsistendReadingBuilder().SetDefaults().Build()
           };

            var expectedText = "province,sensorTypeName,1,ng_m3,1";

            var stream = new MemoryStream();

            sut.CalculateAverageAndWriteToFile(consistentReadings, stream);
            stream.Close();

            string resultText = Encoding.UTF8.GetString(stream.ToArray()).Trim();
            Assert.AreEqual(expectedText, resultText);

        }
    }
}
