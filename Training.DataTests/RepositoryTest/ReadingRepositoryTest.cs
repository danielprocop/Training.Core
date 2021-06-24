using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Training.Data;
using Training.Data.Reading;
using Training.DataTests.Builders;

namespace Training.DataTests.RepositoryTest
{
    [TestClass]
    public class ReadingRepositoryTest:DataBaseHelper
    {
        private readonly IReadingRepository repository;

        public ReadingRepositoryTest()
        {
            repository = new ReadingRepository();
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
            var inputFileId = InsertOneInputFileRecord();
            var dto = new ReadingInsertDtoBuilder()
                .SetInputFileId(inputFileId)
                .Build();
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
                    context.Query<dynamic>("select InputFileId, SensorId, SensorTypeName, Unit, StationId, StationName, Value,Province,City,IsHystoric,StartDate,StopDate" +
                    ",UtmNord,UtmEst,Latitude,Longitude,CreationDate,LastUpdateTime,LastUpdateUser from dbo.Reading");
                Assert.AreEqual(1, items.Count);

                var retrievedItem = items[0];
                AssertHelper.AreEqual(dto, retrievedItem);
                Assert.AreEqual(currentTime, retrievedItem.CreationDate);
                Assert.AreEqual(currentTime, retrievedItem.LastUpdateTime);
                Assert.AreEqual(currentUser, retrievedItem.LastUpdateUser);
            }
        }

        [TestMethod]
        public void Update_FullUpdateDTO_Committed()
        {
            var inputFileId = InsertOneInputFileRecord();
            var readingId = InsertOneReadingRecord(inputFileId);

            var dto = new ReadingUpdateDto(readingId, Status.Error, new List<string>() { "err1", "err2" });
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
                IList<dynamic> readings =
                    context.Query<dynamic>("select Status,LastUpdateTime,LastUpdateUser from dbo.Reading");
                IList<dynamic> messages =
                context.Query<dynamic>("select ReadingId, CreationDate,LastUpdateDate,LastUpdateUser,Message from dbo.ReadingMessage");

                Assert.AreEqual(1, readings.Count);
                Assert.AreEqual(2, messages.Count);
                var retrievedItem = readings[0];
                Assert.AreEqual((int)dto.Status, retrievedItem.Status);
                Assert.AreEqual(currentTime, retrievedItem.LastUpdateTime);
                Assert.AreEqual(currentUser, retrievedItem.LastUpdateUser);
                Assert.AreEqual("err1", messages[0].Message);
                Assert.AreEqual("err2", messages[1].Message);
            }
        }
    }
}
