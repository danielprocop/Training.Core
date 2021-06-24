using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Core;
using Training.Core.ProvinceData;

namespace Training.CoreTests
{
    [TestClass]
    public class AverageProvinceDataFactoryTest
    {
        [TestMethod]
        public void CreateProvinceAverageData_ValidProvinceData_ReturnResultWithSuccess()
        {
            AverageProvinceData expectedAverageProvinceData = new AverageProvinceData("milano", "ses1", 300, Unit.ng_m3,1);
            ProvinceData provinceData = new ProvinceDataBuider()
                .SetProvince("milano")
                .SetSensorTypeName("ses1")
                .SetConsistentReadings(new List<ConsistentReading>() {
                    new ConsistendReadingBuilder().SetDefaults().SetSensorId(111).SetSensorTypeName("ses1").SetProvince("milano").SetValue(500).Build(),
                    new ConsistendReadingBuilder().SetDefaults().SetSensorId(111).SetSensorTypeName("ses1").SetProvince("milano").SetValue(100).Build(),
                }).Build();
            IAverageProvinceDataFactory sut = new AverageProvinceDataFactory();

           var result= sut.CreateProvinceAverageData(provinceData);

            Assert.IsTrue(result.Success);
            AssertHelper.AreEqual(expectedAverageProvinceData, result.Value);
        }


        [TestMethod]
        public void CreateProvinceAverageData_NullInput_ThrowArgumentNullException()
        {
            ProvinceData provinceData = null;
            IAverageProvinceDataFactory sut = new AverageProvinceDataFactory();
            try
            {
                sut.CreateProvinceAverageData(provinceData);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No exception was throw");

        }
        [TestMethod]
        public void CreateProvinceAverageData_ProvinceDataWithoutListOfConsistentReading_ReturnResultWithSuccess()
        {
            ProvinceData provinceData = new ProvinceDataBuider()
                .SetProvince("milano")
                .SetSensorTypeName("ses1")
                .SetConsistentReadings(new List<ConsistentReading>())
                .Build();
            IAverageProvinceDataFactory sut = new AverageProvinceDataFactory();

            try
            {
                var result = sut.CreateProvinceAverageData(provinceData);
            }
            catch (ArgumentException ex)
            {

                StringAssert.Contains(ex.Message, "the provinceData must have al least one record of consistent reading");
                return;
            }


            Assert.Fail("No exception was throw");
        
        }


        [TestMethod]
        public void CreateProvinceAverageData_ProvinceDataUnitDiferentUnit_ReturnResultWithErrors()
        {
            ProvinceData provinceData = new ProvinceDataBuider()
                .SetProvince("milano")
                .SetSensorTypeName("ses1")
                .SetConsistentReadings(new List<ConsistentReading>() {
                new ConsistendReadingBuilder()
                    .SetDefaults()
                    .SetUnit(Unit.mg_m3)
                    .Build(),
                new ConsistendReadingBuilder()
                    .SetDefaults()
                    .SetUnit(Unit.µg_m3)
                    .Build(),
                }).Build();
            IAverageProvinceDataFactory sut = new AverageProvinceDataFactory();

            var result = sut.CreateProvinceAverageData(provinceData);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count);
            StringAssert.Contains(result.Errors[0], "Error: it does not make sense make average calculations if the units of measure of the items are different from one another");
        }

    }
}
