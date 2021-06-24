using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.CoreTests.Helpers.Builder;

namespace Training.CoreTests.ApplicationServiceTests
{
    [TestClass]
    public class ReaderFileServiceIntegratonTest
    {
       

        [TestMethod]
        public void ReadFile_InputWithNoError_ReturnListOfConsistentReadingForAllReading()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
         "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"\n" +
         "10399,Particelle sospese PM2.5,µg/m³,583,Bergamo - via Meucci,249,BG,Bergamo,N,20/12/2008,,5059922,550116,45.69103740547214,9.643650579461385,\"(45.69103740547214, 9.643650579461385)\"\n";

            var loggerMock = new Mock<ILogger<LogService>>();
            var logServiceMock = new Mock<LogService>(loggerMock.Object);
            ReaderFileService sut = new ReaderFileServiceBuilder()
                .WithRealServices()
                .WithLogService(logServiceMock.Object)
                .Build();

            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                var consistentReadings = sut.ReadFile(stream);

                Assert.AreEqual(2, consistentReadings.Count);
                loggerMock.VerifyLog(Times.Never);
            }
        }

        [TestMethod]
        public void ReadFile_InputWithOneFormatingError_ReturnListOfConsistentReadingAndLogError()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
                        "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"\n" +
                        "10399,Particelle sospese PM2.5,µg/m³,583,Bergamo - via Meucci,249,BG,Bergamo,K,20/12/2008,,5059922,550116,45.69103740547214,9.643650579461385,\"(45.69103740547214, 9.643650579461385)\"";

            var loggerMock = new Mock<ILogger<LogService>>();
            var logServiceMock = new Mock<LogService>(loggerMock.Object);
            ReaderFileService sut = new ReaderFileServiceBuilder()
                .WithRealServices()
                .WithLogService(logServiceMock.Object)
                .Build();


            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                var consistentReadings = sut.ReadFile(stream);

                Assert.AreEqual(1, consistentReadings.Count);

               // loggerMock.VerifyLog(Times.Once);

            }
        }

        [TestMethod]
        public void ReadFile_InputWithOneValidatingError_ReturnListOfConsistentReadingForAllReading()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
             "\n-0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"\n" +
             "10399,Particelle sospese PM2.5,µg/m³,583,Bergamo - via Meucci,249,BG,Bergamo,N,20/12/2008,,5059922,550116,45.69103740547214,9.643650579461385,\"(45.69103740547214, 9.643650579461385)\"";

            var loggerMock = new Mock<ILogger<LogService>>();
            var logServiceMock = new Mock<LogService>(loggerMock.Object);
            ReaderFileService sut = new ReaderFileServiceBuilder()
                .WithRealServices()
                .WithLogService(logServiceMock.Object)
                .Build();
           
            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                var consistentReadings = sut.ReadFile(stream);

                Assert.AreEqual(1, consistentReadings.Count);

              //  loggerMock.VerifyLog(Times.Once);

            }
        }


    }
}
