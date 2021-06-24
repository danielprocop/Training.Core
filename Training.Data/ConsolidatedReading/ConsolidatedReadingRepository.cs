using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Data.Reading;

namespace Training.Data
{
    public class ConsolidatedReadingRepository : IConsolidatedReadingRepository
    {
        public void Insert(ConsolidatedReadingInsertDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                insert into dbo.ConsolidatedReading (ReadingId,Status, SensorId, SensorTypeName, Unit, Value,Province,City,IsHystoric,DaysOfMeasure,UtmNord,UtmEst,Latitude,Longitude,CreationDate,LastUpdateDate,LastUpdateUser)
                values (@ReadingId,@Status, @SensorId, @SensorTypeName, @Unit, @Value,@Province,@City,@IsHystoric,@DaysOfMeasure,@UtmNord,@UtmEst,@Latitude,@Longitude,@CreationDate,@LastUpdateDate,@LastUpdateUser)
            ";
            context.Execute(sql,
                new
                {
                    dto.ReadingId,
                    dto.Status,
                    dto.SensorId,
                    dto.SensorTypeName,
                    dto.Unit,
                    dto.Value,
                    dto.Province,
                    dto.City,
                    dto.IsHystoric,
                    dto.DaysOfMeasure,
                    dto.UtmNord,
                    dto.UtmEst,
                    dto.Latitude,
                    dto.Longitude,
                    CreationDate = SystemTime.Now(),
                    LastUpdateDate = SystemTime.Now(),
                    LastUpdateUser = SystemIdentity.CurrentName()
                });
        }

        public void Update(ConsolidatedReadingUpdateDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                update  dbo.ConsolidatedReading set  Status=@Status, LastUpdateDate= @LastUpdateDate, LastUpdateUser=@LastUpdateUser
                where Id=@Id";

            context.Execute(sql,
               new
               {
                   dto.Status,
                   LastUpdateDate = SystemTime.Now(),
                   Id = dto.ConsolidatedReadingId,
                   LastUpdateUser = SystemIdentity.CurrentName()
               });

            const string sqlInsert= @"
                insert into dbo.ConsolidatedReadingFile (ConsolidatedReadingId,OutputFileId, CreationDate, LastUpdateDate, LastUpdateUser)
                values (@ConsolidatedReadingId,@OutputFileId, @CreationDate, @LastUpdateDate, @LastUpdateUser)";

          
            context.Execute(sqlInsert, new
            {
                dto.ConsolidatedReadingId,
                dto.OutputFileId,
                CreationDate = SystemTime.Now(),
                LastUpdateDate = SystemTime.Now(),
                LastUpdateUser = SystemIdentity.CurrentName(),
            });

        }

        public IList<ConsolidatedReadingGetDto> GetByStatus(Status status, IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return context.Query<ConsolidatedReadingGetDto>("select Id ,Status, SensorId,SensorTypeName,Unit,Value,Province,City,IsHystoric,DaysOfMeasure,UtmNord,UtmEst,Latitude,Longitude  from dbo.ConsolidatedReading where Status=@Status order by CreationDate", new { Status = status });
        }
    }
}
