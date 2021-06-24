using System.Collections.Generic;
using Training.Data;
using Training.Data.Reading;

namespace ConsoleWithDb.Services
{
    public interface IConsolidatedReadingService
    {
        void CreateConsolidatedReading(ReadingGetDto readingDto);
        List<ConsolidatedReadingGetDto> GetConsolidatedReadingsFromDb();
    }
}