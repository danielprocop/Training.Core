using System.Collections.Generic;

namespace Training.Data
{
    public interface IInputFileRepository
    {
        void Insert(InputFileInsertDto dto, IContext context);
        void Update(InputFileUpdateDto dto, IContext context);
        IList<InputFileGetDto> GetByStatus(Status status, IContext context);
        IList<InputFileGetDto> GetByStatusAndOrderByCreationDate(Status status, IContext context);
    }

}
