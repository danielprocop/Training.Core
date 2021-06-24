using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Data;
using Training.DataTests.Builders;

namespace Training.DataTests.RepositoryTest
{
    public class DataBaseHelper
    {
        protected readonly IContextFactory contextFactory;
        public DataBaseHelper()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

            string connectionString = configuration.GetConnectionString("Default");
            contextFactory = new DapperContextFactory(connectionString);
        }

        public void CleanDatabase()
        {
            using (IContext context = contextFactory.Create())
            {
                context.Execute("delete from dbo.ConsolidatedReadingFile");
                context.Execute("delete from dbo.ConsolidatedReading");
                context.Execute("delete from dbo.OutputFile");
                context.Execute("delete from dbo.ReadingMessage");
                context.Execute("delete from dbo.Reading");
                context.Execute("delete from dbo.InputFileMessage");
                context.Execute("delete from dbo.InputFile");
                context.Commit();
            }
        } 
        public long InsertOneInputFileRecord()
        {
            var dto = new InputFileInsertDto("filename1", new byte[] { 1, 2, 3 }, "ext", Status.New);
            var currentTime = new DateTime(2020, 05, 12);
            const string currentUser = "currentUser1";
            SystemTime.Now = () => currentTime;
            SystemIdentity.CurrentName = () => currentUser;


            using (IContext context = contextFactory.Create())
            {
                const string sql = @"
                insert into dbo.InputFile (Name, Extension, Bytes, StatusId, CreationTime, LastUpdateTime, LastUpdateUser)
                OUTPUT Inserted.InputFileId 
                values (@Name, @Extension, @Bytes, @Status, @CreationTime, @LastUpdateTime, @LastUpdateUser)
            ";
                var id = context.ExecuteScalar<long>(sql,
                     new
                     {
                         dto.Name,
                         dto.Extension,
                         dto.Bytes,
                         dto.Status,
                         CreationTime = SystemTime.Now(),
                         LastUpdateTime = SystemTime.Now(),
                         LastUpdateUser = SystemIdentity.CurrentName()
                     });
                context.Commit();
                return id;
            }
        }

        public int InsertOneReadingRecord(long inputFileId)
        {
            var dto = new ReadingInsertDtoBuilder()
                 .SetInputFileId(inputFileId)
                 .Build();

            var currentTime = new DateTime(2020, 05, 12);
            const string currentUser = "currentUser1";
            SystemTime.Now = () => currentTime;
            SystemIdentity.CurrentName = () => currentUser;


            using (IContext context = contextFactory.Create())
            {
                const string sql = @"
                insert into dbo.Reading (InputFileId,Status, SensorId, SensorTypeName, Unit, StationId, StationName, Value,Province,City,IsHystoric,StartDate,StopDate,UtmNord,UtmEst,Latitude,Longitude,CreationDate,LastUpdateTime,LastUpdateUser)
                   OUTPUT Inserted.Id 
                values (@InputFileId,@Status, @SensorId, @SensorTypeName, @Unit, @StationId, @StationName, @Value,@Province,@City,@IsHystoric,@StartDate,@StopDate,@UtmNord,@UtmEst,@Latitude,@Longitude,@CreationDate,@LastUpdateTime,@LastUpdateUser)
            ";
               
                var id = context.ExecuteScalar<int>(sql,
                     new
                     {
                         dto.InputFileId,
                         dto.Status,
                         dto.SensorId,
                         dto.SensorTypeName,
                         dto.Unit,
                         dto.StationId,
                         dto.StationName,
                         dto.Value,
                         dto.Province,
                         dto.City,
                         dto.IsHystoric,
                         dto.StartDate,
                         dto.StopDate,
                         dto.UtmNord,
                         dto.UtmEst,
                         dto.Latitude,
                         dto.Longitude,
                         CreationDate = SystemTime.Now(),
                         LastUpdateTime = SystemTime.Now(),
                         LastUpdateUser = SystemIdentity.CurrentName()
                     });
                context.Commit();
                return id;
            }
        }
    }
}
