using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Data.Reading
{
    public class ReadingUpdateDto
    {
        public ReadingUpdateDto(int readingId, Status status, IList<string> messages)
        {
            ReadingId = readingId;
            Status = status;
            Messages = messages;
        }

        public int ReadingId { get; }
        public Status Status { get; }
        public IList<string> Messages { get; }
    }
}
