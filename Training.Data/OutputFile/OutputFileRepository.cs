using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Data
{
    public class OutputFileRepository : IOutputFileRepository
    {
        public int Insert(OutputFileInsertDto dto, IContext context)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (context == null) throw new ArgumentNullException(nameof(context));

            const string sql = @"
                insert into dbo.OutputFile (Bytes, CreationDate, LastUpdateDate, LastUpdateUser)
                OUTPUT Inserted.Id 
                values (@Bytes, @CreationDate, @LastUpdateDate, @LastUpdateUser)
            ";
            return context.ExecuteScalar<int>(sql,
                new
                {
                    dto.Bytes,
                    CreationDate = SystemTime.Now(),
                    LastUpdateDate = SystemTime.Now(),
                    LastUpdateUser = SystemIdentity.CurrentName()
                });
        }
       
    }

}
