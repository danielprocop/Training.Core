using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Data;
using Training.DataTests.RepositoryTest;

namespace Training.DataTests
{
    [TestClass]
    public class Session8Tests: DataBaseHelper
    {
       
        private readonly IInputFileRepository repository;

        public Session8Tests()
        {
            repository = new InputFileRepository();

            CleanDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanDatabase();
        }

        [TestMethod]
        public void Insert_FullInsertDTO_Committed()
        {
            // Arrange
            var dto = new InputFileInsertDto("filename1", new byte[] { 1, 2, 3 }, "ext", Status.New);
            var currentTime = new DateTime(2020, 05, 12);
            const string currentUser = "currentUser1";
            SystemTime.Now = () => currentTime;
            SystemIdentity.CurrentName = () => currentUser;
            using (IContext context = contextFactory.Create())
            {
                // Act
                repository.Insert(dto, context);
                context.Commit();
            }
            // Assert
            using (IContext context = contextFactory.Create())
            {
                IList<dynamic> items =
                    context.Query<dynamic>("select Name,Extension,Bytes,StatusId,CreationTime,LastUpdateTime,LastUpdateUser from dbo.InputFile");
                Assert.AreEqual(1, items.Count);

                var retrievedItem = items[0];
                Assert.AreEqual(dto.Name, retrievedItem.Name);
                Assert.AreEqual(dto.Extension, retrievedItem.Extension);
                Assert.IsTrue(dto.Bytes.SequenceEqual((byte[])retrievedItem.Bytes));
                Assert.AreEqual((int)dto.Status, retrievedItem.StatusId);
                Assert.AreEqual(currentTime, retrievedItem.CreationTime);
                Assert.AreEqual(currentTime, retrievedItem.LastUpdateTime);
                Assert.AreEqual(currentUser, retrievedItem.LastUpdateUser);
            }
        }

        [TestMethod]
        public void Update_FullUpdateDTO_Committed()
        {
            var id=InsertOneRecord();

            var dto = new InputFileUpdateDto(id,Status.Error,new List<string>() { "err1","err2"});
            var currentTime = new DateTime(2021, 06, 12);
            const string currentUser = "currentUserPippo";
            SystemTime.Now = () => currentTime;
            SystemIdentity.CurrentName = () => currentUser;
            using (IContext context = contextFactory.Create())
            {
                // Act
                repository.Update(dto, context);
                context.Commit();
            }
            // Assert
            using (IContext context = contextFactory.Create())
            {
                IList<dynamic> items =
                    context.Query<dynamic>("select Name,Extension,Bytes,StatusId,CreationTime,LastUpdateTime,LastUpdateUser from dbo.InputFile");
                IList<dynamic> messages =
                context.Query<dynamic>("select InputFileId, CreationDate,LastUpdateDate,LastUpdateUser,Message from dbo.InputFileMessage");

                Assert.AreEqual(1, items.Count);
                Assert.AreEqual(2, messages.Count);
                var retrievedItem = items[0];
                Assert.AreEqual((int)dto.Status, retrievedItem.StatusId);
                Assert.AreEqual(currentTime, retrievedItem.LastUpdateTime);
                Assert.AreEqual(currentUser, retrievedItem.LastUpdateUser);
                Assert.AreEqual("err1", messages[0].Message);
                Assert.AreEqual("err2", messages[1].Message);
            }
        }

        [TestMethod]
        public void GetByStatus()
        {
            //testare sempre con 0,1,2 record nella tabella
            InsertOneRecord();

            using (IContext context = contextFactory.Create())
            {
                // Act
                var items=repository.GetByStatus(Status.New, context);
                Assert.AreEqual(1, items.Count);

                var retrievedItem = items[0];
                Assert.AreEqual("filename1", retrievedItem.Name);
                Assert.AreEqual("ext", retrievedItem.Extension);
                Assert.IsTrue(new byte[] { 1, 2, 3 }.SequenceEqual((byte[])retrievedItem.Bytes));
                Assert.AreEqual(Status.New, retrievedItem.Status);
            }
        }


        private long InsertOneRecord()
        {
            var dto = new InputFileInsertDto("filename1", new byte[] { 1, 2, 3 }, "ext", Status.New);
            var currentTime = new DateTime(2020, 05, 12);
            const string currentUser = "currentUser1";
            SystemTime.Now = () => currentTime;
            SystemIdentity.CurrentName = () => currentUser;
            using (IContext context = contextFactory.Create())
            {
                repository.Insert(dto, context);
                context.Commit();

                //TOdo usare direttamente dapper.Insert
                var record = context.Query<dynamic>("select InputFileId from dbo.InputFile").FirstOrDefault();
                return record.InputFileId;
            }
        }
    }

}
