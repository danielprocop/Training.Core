using System;
using System.Collections.Generic;
using System.Text;
using Training.Data.Reading;

namespace Training.Data
{
    public interface IReadingRepository
    {
        void Insert(ReadingInsertDto dto, IContext context);
        void Update(ReadingUpdateDto dto, IContext context);
        IList<ReadingGetDto> GetByStatus(Status status, IContext context);
    }
}
