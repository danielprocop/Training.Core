using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Core;
using Training.Core.ApplicationServices;
using Training.CoreTests.Helpers.Builder;

namespace Training.CoreTests.ApplicationServiceTests
{
    [TestClass]
    public class ReaderFileServiceTest
    {
        const string text= "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
        "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"\n" +
        "10399,Particelle sospese PM2.5,µg/m³,583,Bergamo - via Meucci,249,BG,Bergamo,N,20/12/2008,,5059922,550116,45.69103740547214,9.643650579461385,\"(45.69103740547214, 9.643650579461385)\"\n";



        [TestMethod]
        public void ReadingsImportService_ImportMethod_IsCalled()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));

            var readingsImportServiceMock = new Mock<IReadingsImportService>();
            ReaderFileService sut = new ReaderFileServiceBuilder()
                .WithReadingsImportService(readingsImportServiceMock.Object)
                .Build();

            readingsImportServiceMock
                .Setup(x => x.Import(stream))
                .Returns(new ImportResult(new List<Reading>(), new List<string>()));

            var consistentReadings = sut.ReadFile(stream);

            readingsImportServiceMock.Verify(x => x.Import(stream));
            stream.Close();
        }

        [TestMethod]
        public void ReadingsImportService_LogErrors_IsCalled()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));

            var readingsImportServiceMock = new Mock<IReadingsImportService>();
            var logServiceMock= new Mock<LogService>(new Mock<ILogger<LogService>>().Object);
            ReaderFileService sut = new ReaderFileServiceBuilder()
                .WithReadingsImportService(readingsImportServiceMock.Object)
                .WithLogService(logServiceMock.Object)
                .Build();

            var errors = new List<string>() { "erro1" };
            readingsImportServiceMock
                .Setup(x => x.Import(stream))
                .Returns(new ImportResult(new List<Reading>(), errors));

            var consistentReadings = sut.ReadFile(stream);

            logServiceMock.Verify(x => x.LogErrors(errors));
            stream.Close();
        }
    }
}
