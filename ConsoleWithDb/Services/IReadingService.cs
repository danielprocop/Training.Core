using System.Collections.Generic;
using Training.Data;
using Training.Data.Reading;

namespace ConsoleWithDb.Services
{
    public interface IReadingService
    {
        void CreateReadingsFromInputFile(InputFileGetDto inputFile);
        List<ReadingGetDto> GetReadingsFromDb();
    }
}