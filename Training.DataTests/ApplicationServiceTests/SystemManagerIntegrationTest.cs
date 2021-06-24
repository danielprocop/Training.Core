using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Training.Data;
using Training.DataTests.Builders;
using Training.DataTests.RepositoryTest;

namespace Training.DataTests.ApplicationServiceTests
{
    [TestClass]
    public class SystemManagerIntegrationTest: DataBaseHelper
    {

        private const string backupPath = @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\BACKUP\";
        private const string inputPath = @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\";
        public SystemManagerIntegrationTest()
        {
            CleanDatabase();
        }
        [TestCleanup]
        public void TestCleanup()
        {

            CleanDatabase();
            DirectoryInfo directoryInfo = new DirectoryInfo(backupPath);
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                File.Move(fileInfo.FullName, Path.Combine(inputPath, fileInfo.Name));
            }
        }
        [TestMethod]
        public void Test_completo()
        {
            var sut = new SystemManagerBuilder().WithRealDependencies().Build();
            sut.Run();

            using (IContext context = contextFactory.Create())
            {
                IList<dynamic> inputFiles =
                  context.Query<dynamic>("select * from dbo.InputFile");
               
                IList<dynamic> inputFileMessages =
                  context.Query<dynamic>("select * from dbo.InputFileMessage");
               
                IList<dynamic> readings =
                    context.Query<dynamic>("select * from dbo.Reading");

                IList<dynamic> readingMessages =
               context.Query<dynamic>("select * from dbo.ReadingMessage");

                IList<dynamic> consolidatedReadings =
                   context.Query<dynamic>("select * from dbo.ConsolidatedReading");

                IList<dynamic> consolidatedReadingFiles =
                 context.Query<dynamic>("select * from dbo.ConsolidatedReadingFile");

                Assert.AreEqual(2, inputFiles.Count);
                Assert.AreEqual(2, inputFileMessages.Count);
                Assert.AreEqual(14,readings.Count);
                Assert.AreEqual(14, consolidatedReadings.Count);
                Assert.AreEqual(0, readingMessages.Count);
                Assert.AreEqual(1, consolidatedReadingFiles.Count);

             
            }
        }
    }
}
