using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Data
{
    public class InputFileRepository : IInputFileRepository
    {
        public void Insert(InputFileInsertDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                insert into dbo.InputFile (Name, Extension, Bytes, StatusId, CreationTime, LastUpdateTime, LastUpdateUser)
                values (@Name, @Extension, @Bytes, @Status, @CreationTime, @LastUpdateTime, @LastUpdateUser)
            ";
            context.Execute(sql,
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
        }
        public void Update(InputFileUpdateDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                update  dbo.InputFile set  StatusId=@Status, LastUpdateTime= @LastUpdateTime, LastUpdateUser=@LastUpdateUser
                where InputFileId=@InputFileId";

            context.Execute(sql,
               new
               {
                   dto.Status,
                   LastUpdateTime = SystemTime.Now(),
                   LastUpdateUser = SystemIdentity.CurrentName(),
                   InputFileId = dto.InputFileId
               });

            const string sqlInsertMessage = @"
                insert into dbo.InputFileMessage (InputFileId, CreationDate, LastUpdateDate, LastUpdateUser, Message)
                values (@InputFileId, @CreationDate, @LastUpdateDate, @LastUpdateUser,@Message)";
            
            foreach (var message in dto.Messages)
            {

                context.Execute(sqlInsertMessage,
                 new
                 {
                     dto.InputFileId,
                     CreationDate = SystemTime.Now(),
                     LastUpdateDate = SystemTime.Now(),
                     LastUpdateUser = SystemIdentity.CurrentName(),
                     Message = message
                 });

            }

        }
        public IList<InputFileGetDto> GetByStatus(Status status, IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return context.Query<InputFileGetDto>("select InputFileId,Name, Bytes,Extension, StatusId as Status from dbo.InputFile where StatusId=@Status", new { Status = status });
           
        }

        public IList<InputFileGetDto> GetByStatusAndOrderByCreationDate(Status status, IContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return context.Query<InputFileGetDto>("select InputFileId,Name, Bytes,Extension, StatusId as Status from dbo.InputFile where StatusId=@Status order by CreationTime", new { Status = status });

        }
    }

}
