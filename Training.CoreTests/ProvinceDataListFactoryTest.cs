using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;

namespace Training.CoreTests
{
    [TestClass]
    public class ProvinceDataListFactoryTest
    {

        [TestMethod]
        public void CreateProvinceDataList_TwoConsistentReadingWithSameProvinceAndSensorTypeName_ReturnOneProvinceData()
        {
            var consistendReading1 = new ConsistendReadingBuilder()
                .SetDefaults()
                .Build();
            var consistendReading2 = new ConsistendReadingBuilder()
               .SetDefaults()
               .SetSensorId(2)
               .SetValue(24)
               .Build();
            List<ConsistentReading> consistentReadings = new List<ConsistentReading>()
            {
                consistendReading1,
                consistendReading2
            };
            ProvinceDataListFactory sut = new ProvinceDataListFactory();

            var provinceData = sut.CreateProvinceDataList(consistentReadings);

            Assert.AreEqual(1, provinceData.Count);
            Assert.AreEqual(2, provinceData[0].ConsistentReadings.Count);
            Assert.AreEqual(consistendReading1.Province, provinceData[0].Province);
            Assert.AreEqual(consistendReading1.SensorTypeName, provinceData[0].SensorTypeName);
        }

        [TestMethod]
        public void CreateProvinceDataList_TwoConsistentReadingWithDiferentProvinceAndSensorTypeName_ReturnTwoProvinceData()
        {
            var consistendReading1 = new ConsistendReadingBuilder()
                .SetDefaults()
                .SetProvince("milano")
                .Build();
            var consistendReading2 = new ConsistendReadingBuilder()
               .SetDefaults()
               .SetProvince("pavia")
               .SetSensorId(2)
               .SetValue(24)
               .Build();
            List<ConsistentReading> consistentReadings = new List<ConsistentReading>()
            {
                consistendReading1,
                consistendReading2
            };
            ProvinceDataListFactory sut = new ProvinceDataListFactory();

            var provinceData = sut.CreateProvinceDataList(consistentReadings);

            Assert.AreEqual(2, provinceData.Count);
            Assert.AreEqual(1, provinceData[0].ConsistentReadings.Count);
            Assert.AreEqual(1, provinceData[1].ConsistentReadings.Count);
            Assert.AreEqual(consistendReading1.Province, provinceData[0].Province);
            Assert.AreEqual(consistendReading2.Province, provinceData[1].Province);
        }


        [TestMethod]
        public void CreateProvinceDataList_NullInput_ThrowArgumentNullException()
        {
            List<ConsistentReading> consistentReadings = null;
            ProvinceDataListFactory sut = new ProvinceDataListFactory();
            try
            {
                sut.CreateProvinceDataList(consistentReadings);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No exception was throw");

        }

        [TestMethod]
        public void CreateProvinceDataList_EmptyListInput_ReturnEmptyList()
        {
            List<ConsistentReading> consistentReadings = new List<ConsistentReading>();
            ProvinceDataListFactory sut = new ProvinceDataListFactory();

           var resultList= sut.CreateProvinceDataList(consistentReadings);

            Assert.AreEqual(0, resultList.Count);
        }
    }
}
