using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using SystemWrapper.Implemetation;
using SystemWrapper.Interface;
using Training.Data;
using Training.DataTests.Builders;

namespace Training.DataTests.ApplicationServiceTests
{
    [TestClass]
    public class InputFileServiceTest
    {


        [TestMethod]
        public void OneFileFound_ErrorOnInsert_NoMoveFile()
        {
          
            var fileMock = new Mock<FileWrap>();
            fileMock.Setup(x => x.ReadAllBytes(It.IsAny<string>())).Returns(new byte[] { 1, 2, 3 });

            var inputFileRepositoryMock = new Mock<IInputFileRepository>();
            inputFileRepositoryMock
                .Setup(x => x.Insert(It.IsAny<InputFileInsertDto>(), It.IsAny<IContext>()))
                .Throws(new InvalidOperationException());

            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.FullName).Returns(@"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\file2.csv");
            fileInfoMock.Setup(x => x.Name).Returns("file2.csv");
            fileInfoMock.Setup(x => x.Extension).Returns(".csv");

            var sut = new InputFileServiceBuilder()
              .WithInputFileRepository(inputFileRepositoryMock.Object)
              .WithFile(fileMock.Object)
              .Build();

            try
            {
                sut.CreateInputFile(fileInfoMock.Object);

            }
            catch (InvalidOperationException)
            {
                fileMock
               .Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");

          

        }

        [TestMethod]
        public void OneFileFound_ErrorOnMove_NoCommit()
        {
         
            var fileMock = new Mock<FileWrap>();
            fileMock
                .Setup(x => x.ReadAllBytes(It.IsAny<string>()))
                .Returns(new byte[] { 1, 2, 3 });
            fileMock
                .Setup(x => x.Move(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new InvalidOperationException());

            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.FullName).Returns(@"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\file2.csv");
            fileInfoMock.Setup(x => x.Name).Returns("file2.csv");
            fileInfoMock.Setup(x => x.Extension).Returns(".csv");

            var contextFactoryMock = new Mock<IContextFactory>();
            var contextMock = new Mock<IContext>();
            contextFactoryMock.Setup(x => x.Create()).Returns(contextMock.Object);

            var sut = new InputFileServiceBuilder()
              .WithFile(fileMock.Object)
              .WithContextFactory(contextFactoryMock.Object)
              .Build();
            try
            {
                sut.CreateInputFile(fileInfoMock.Object);

            }
            catch (InvalidOperationException)
            {
                contextMock.Verify(x => x.Commit(), Times.Never);
               return ;
            }
            Assert.Fail("The expected exception was not thrown.");




        }

        [TestMethod]
        public void OneFileFound_InsertOnDb_MoveFileInBackup()
        {
            var directoryInfoMock = new Mock<IDirectoryInfo>();
            directoryInfoMock
                .Setup(x => x.GetFiles())
                .Returns(new FileInfoWrap[1] { new FileInfoWrap("c://file1", "file1", "txt") });

            var fileMock = new Mock<FileWrap>();
            fileMock
                .Setup(x => x.ReadAllBytes(It.IsAny<string>()))
                .Returns(new byte[] { 1, 2, 3 });
            var inputFileRepositoryMock = new Mock<IInputFileRepository>();

           

            var contextFactoryMock = new Mock<IContextFactory>();
            var contextMock = new Mock<IContext>();
            contextFactoryMock.Setup(x => x.Create()).Returns(contextMock.Object);
            inputFileRepositoryMock.Setup(x => x.Insert(It.IsAny<InputFileInsertDto>(), contextMock.Object));
    

           var sut = new InputFileServiceBuilder()
              .WithInputFileRepository(inputFileRepositoryMock.Object)
              .WithFile(fileMock.Object)
              .WithContextFactory(contextFactoryMock.Object)
              .Build();

            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.FullName).Returns(@"C:\Users\Daniel Procop\source\repos\Training.Core\ConsoleWithDb\In\file2.csv");
            fileInfoMock.Setup(x => x.Name).Returns("file2.csv");
            fileInfoMock.Setup(x => x.Extension).Returns(".csv");

            sut.CreateInputFile(fileInfoMock.Object);

            //verificare che il metodi sia chiamto con i valori aspettati
            //It.IsAny<string>() da sostituire
            fileMock
             .Verify(x => x.Move(It.IsAny<string>(), It.IsAny<string>()));

          //usare il metodo callback sul mock oppure fare un servizio che restituisca l'oggetto
            inputFileRepositoryMock.Verify(x => x.Insert(dtoExpected, contextMock.Object));
        }
    }
}
