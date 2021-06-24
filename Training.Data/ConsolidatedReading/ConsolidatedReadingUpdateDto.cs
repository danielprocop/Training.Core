using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Data.Reading
{
    public class ConsolidatedReadingUpdateDto
    {
        public ConsolidatedReadingUpdateDto(int consolidatedReadingId,int outputFileId, Status status)
        {
            ConsolidatedReadingId = consolidatedReadingId;
            Status = status;
            OutputFileId = outputFileId;
        }

        public int ConsolidatedReadingId { get; }
        public int OutputFileId { get; }
        public Status Status { get; }
    }
}
