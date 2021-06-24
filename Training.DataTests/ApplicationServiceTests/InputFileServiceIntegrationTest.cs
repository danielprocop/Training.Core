using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemWrapper.Interface;
using Training.Data;
using Training.DataTests.Builders;
using Training.DataTests.RepositoryTest;

namespace Training.DataTests.ApplicationServiceTests
{
    [TestClass]
    public class InputFileServiceIntegrationTest : DataBaseHelper
    {
        private const string backupPath = @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\BACKUP\";
        private const string inputPath = @"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\";
        private readonly IInputFileRepository repository;
        public InputFileServiceIntegrationTest ()
        {

            repository = new InputFileRepository();
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
        public void Test()
        {

            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.FullName).Returns(@"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\file2.csv");
            fileInfoMock.Setup(x => x.Name).Returns("file2.csv");
            fileInfoMock.Setup(x => x.Extension).Returns(".csv");
            
            var sut = new InputFileServiceBuilder()
            .WithRealDependency()
            .Build();

            sut.CreateInputFile(fileInfoMock.Object);

            using (IContext context = contextFactory.Create())
            {
                // Act
                var items = repository.GetByStatus(Status.New, context);
                Assert.AreEqual(1, items.Count);

            }

            DirectoryInfo directoryInfo = new DirectoryInfo(backupPath);
            Assert.AreEqual(1, directoryInfo.GetFiles().Count());
        }
    }
}
