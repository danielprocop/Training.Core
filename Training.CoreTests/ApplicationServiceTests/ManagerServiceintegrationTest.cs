using FileHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.Core.ProvinceData;
using Training.CoreTests.Helpers.Builder;

namespace Training.CoreTests.ApplicationServiceTests
{
    [TestClass]
    public class ManagerServiceintegrationTest
    {
        [TestMethod]
        public void Test()
        {

            var logggerMock = new Mock<ILogger<ManagerService>>();
            var dataAggregatorService = new DataAggregatorServiceBuilder().WithRealServices().Build();
            var readerFileService = new ReaderFileServiceBuilder().WithRealServices().Build();
            var options = Options.Create(new Paths()
            {
                InputPath = @"C:\Users\Daniel Procop\source\repos\Training.Core\Training.CoreTests\InputPath\",
                OutputFile = @"C:\Users\Daniel Procop\source\repos\Training.Core\Training.CoreTests\OutputPath\out.csv"
            });
            ManagerService managerService = new ManagerService(logggerMock.Object, dataAggregatorService, readerFileService, options);
            managerService.Run();

            //aggiungere header per file output
            var expectedText = "MN,Nikel,122,ng_m3,2846";
            string text = File.ReadAllText(@"C:\Users\Daniel Procop\source\repos\Training.Core\Training.CoreTests\OutputPath\out.csv").Trim();


            Assert.AreEqual(expectedText, text);
        }
    }
}
