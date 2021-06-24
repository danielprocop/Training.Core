using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using Training.Core;

namespace Training.CoreTests
{
    [TestClass]
    public class ReadingServiceTest
    {
        [TestMethod]
        public void Import_NullStream_ThrowException()
        {
            ReadingsImportService readerService = new ReadingsImportService();
            try
            {
                readerService.Import(null);
            }
            catch (System.ArgumentNullException)
            {

                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void Import_InvalidHeader_ThrowException()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location,XXX";
            ReadingsImportService readerService = new ReadingsImportService();

            try
            {
                using (var stream = new MemoryStream(
               Encoding.UTF8.GetBytes(text)))
                {
                    readerService.Import(stream);
                }
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, ReadingsImportService.ErrorHeader);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");

        }

        [TestMethod]
        public void Import_ValidRecord_ReturnOneReadingWithNoError()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"";
            Reading expectedReading = new Reading(0177, "Ossidi di Azoto", "µg_m3", 704, "Sermide - via Dalla Chiesa",11,"MN", "Sermide e Felonica",true,new System.DateTime(2006,11,29), new System.DateTime(2017, 1, 1), 4986023, 680789, "45.004613731114105", "11.294014707706845");
            ReadingsImportService readerService = new ReadingsImportService();
            ImportResult importResult;
            
            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                importResult = readerService.Import(stream);

            }

            Assert.IsTrue(importResult.Success);
            AssertHelper.AreEqual(expectedReading, importResult.Readings[0]);

        }

        [TestMethod]
        public void Import_InvalidBooleanField_ReturnError()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,N,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica, ,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,K,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"";
            ReadingsImportService readerService = new ReadingsImportService();

            ImportResult importResult;
            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                importResult= readerService.Import(stream);

            }

            Assert.IsFalse(importResult.Success);
            Assert.AreEqual(importResult.Errors.Count, 3);
            Assert.AreEqual(importResult.Readings.Count, 2);
            foreach (var error in importResult.Errors)
            {
                StringAssert.Contains(error, "Storico");
            }
           
        }

        [TestMethod]
        public void Import_InvalidStartDateField_ReturnError()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S, ,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29112006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"";
            ReadingsImportService readerService = new ReadingsImportService();

            ImportResult importResult;
            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                importResult = readerService.Import(stream);

            }

            Assert.IsFalse(importResult.Success);
            Assert.AreEqual(importResult.Errors.Count, 3);
            Assert.AreEqual(importResult.Readings.Count, 1);
            foreach (var error in importResult.Errors)
            {
                StringAssert.Contains(error, "DataStart");
            }
        }

        [TestMethod]
        public void Import_InvalidStopDateField_ReturnError()
        {
            var text = "IdSensore,NomeTipoSensore,UnitaMisura,Idstazione,NomeStazione,Quota,Provincia,Comune,Storico,DataStart,DataStop,Utm_Nord,UTM_Est,lat,lng,location" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,01/01/2017,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006, ,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"" +
                 "\n0177,Ossidi di Azoto,µg/m³,704,Sermide - via Dalla Chiesa,11,MN,Sermide e Felonica,S,29/11/2006,0,4986023,680789,45.004613731114105,11.294014707706845,\"(45.004613731114105, 11.294014707706845)\"";
            ReadingsImportService readerService = new ReadingsImportService();

            ImportResult importResult;
            using (var stream = new MemoryStream(
                Encoding.UTF8.GetBytes(text)))
            {
                importResult = readerService.Import(stream);

            }

            Assert.IsFalse(importResult.Success);
            Assert.AreEqual(importResult.Errors.Count, 1);
            Assert.AreEqual(importResult.Readings.Count, 3);
            foreach (var error in importResult.Errors)
            {
                StringAssert.Contains(error, "DataStop");
            }
        }
    }
}
