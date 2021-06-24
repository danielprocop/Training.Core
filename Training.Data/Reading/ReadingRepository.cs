using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Data.Reading;

namespace Training.Data
{
    public class ReadingRepository : IReadingRepository
    {
        public void Insert(ReadingInsertDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                insert into dbo.Reading (InputFileId,Status, SensorId, SensorTypeName, Unit, StationId, StationName, Value,Province,City,IsHystoric,StartDate,StopDate,UtmNord,UtmEst,Latitude,Longitude,CreationDate,LastUpdateTime,LastUpdateUser)
                values (@InputFileId,@Status, @SensorId, @SensorTypeName, @Unit, @StationId, @StationName, @Value,@Province,@City,@IsHystoric,@StartDate,@StopDate,@UtmNord,@UtmEst,@Latitude,@Longitude,@CreationDate,@LastUpdateTime,@LastUpdateUser)
            ";
            context.Execute(sql,
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
        }

        public void Update(ReadingUpdateDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                update  dbo.Reading set  Status=@Status, LastUpdateTime= @LastUpdateTime, LastUpdateUser=@LastUpdateUser
                where Id=@Id";

            context.Execute(sql,
               new
               {
                   dto.Status,
                   LastUpdateTime = SystemTime.Now(),
                   LastUpdateUser = SystemIdentity.CurrentName(),
                   Id = dto.ReadingId
               });

            const string sqlInsertMessage = @"
                insert into dbo.ReadingMessage (ReadingId, CreationDate, LastUpdateDate, LastUpdateUser, Message)
                values (@ReadingId, @CreationDate, @LastUpdateDate, @LastUpdateUser,@Message)";

          
           var messageList= dto.Messages.Select(message=> new 
                 {
                     dto.ReadingId,
                     CreationDate = SystemTime.Now(),
                     LastUpdateDate = SystemTime.Now(),
                     LastUpdateUser = SystemIdentity.CurrentName(),
                     Message = message
            }).ToList();

            context.Execute(sqlInsertMessage, messageList);

        }

        public IList<ReadingGetDto> GetByStatus(Status status, IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return context.Query<ReadingGetDto>("select Id ,Status, SensorId,SensorTypeName,Unit,StationId,StationName,Value,Province,City,IsHystoric,StartDate,StopDate,UtmNord,UtmEst,Latitude,Longitude  from dbo.Reading where Status=@Status order by CreationDate", new { Status = status });

        }
    }
}
