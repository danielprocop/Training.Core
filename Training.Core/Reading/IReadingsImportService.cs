using System.IO;

namespace Training.Core
{
    public interface IReadingsImportService
    {
        ImportResult Import(Stream stream);
    }

}
