using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Core.ProvinceData;
using Training.CoreTests.Helpers.Builder;

namespace Training.CoreTests
{
    [TestClass]
    public class AverageProvinceDataExportServiceTest
    {

        [TestMethod]
        public void Test()
        {
            IList<AverageProvinceData> averageProvinceDatas = new List<AverageProvinceData>()
            {
                new AverageProvinceDataBuider().SetDefaults().Build()
            };
            var expectedText = "prov,sens01,1,ng_m3,1";
            var sut = new AverageProvinceDataExportService();

            var stream = new MemoryStream();

            sut.Export(averageProvinceDatas, stream);
            stream.Close();
           
            string resultText = Encoding.UTF8.GetString(stream.ToArray()).Trim();
            Assert.AreEqual(expectedText, resultText);

        }
    }
}
