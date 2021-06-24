using System;
using System.Collections.Generic;
using System.Text;
using Training.Data.Reading;

namespace Training.Data
{
    public interface IConsolidatedReadingRepository
    {
        void Insert(ConsolidatedReadingInsertDto dto, IContext context);
        void Update(ConsolidatedReadingUpdateDto dto, IContext context);
        IList<ConsolidatedReadingGetDto> GetByStatus(Status status, IContext context);
    }
}
