namespace Training.Core
{
    public interface IConsistentReadingFactory
    {
        Result<ConsistentReading> CreateConsistentReading(Reading reading);
    }

}
