using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Training.CoreTests.ApplicationServiceTests
{
    public static class ExtensionMock
    {

        public static void VerifyLog<T>(this Mock<ILogger<T>> mockLogger, Func<Times> times)
        {
            mockLogger.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times);
        }
    }
}
