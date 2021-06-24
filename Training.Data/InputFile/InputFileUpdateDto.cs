using System.Collections.Generic;

namespace Training.Data
{
    public class InputFileUpdateDto
    {
        public InputFileUpdateDto(long inputFileId, Status status, IList<string> messages)
        {
            InputFileId = inputFileId;
            Status = status;
            Messages = messages;
        }

        public long InputFileId { get; }
        public Status Status { get; }
        public IList<string> Messages { get; }
    }

}
