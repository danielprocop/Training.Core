using System.Collections.Generic;

namespace Training.Data
{
    public interface IOutputFileRepository
    {
        int Insert(OutputFileInsertDto dto, IContext context);
   
    }

}
