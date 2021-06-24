using System.Collections.Generic;
using Training.Data;

namespace ConsoleWithDb.Services
{
    public interface IOutputFileService
    {
        void CreateOutputFileFromConsistentReadings(List<ConsolidatedReadingGetDto> consolidatedReadings);
    }
}