using Common.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Common.Tests.Http
{
    public class HttpWrapperTests
    {
        private readonly Mock<ILogger<HttpWrapper>> _mockLogger;
        private readonly IHttpWrapper _objectToTest;
        public HttpWrapperTests()
        {
            _mockLogger = new Mock<ILogger<HttpWrapper>>();
            _objectToTest = new HttpWrapper(_mockLogger.Object);
        }

        public class Http : HttpWrapperTests
        {
            [Theory]
            [InlineData("https://api.protonvpn.ch/vpn/location")]
            public void Returns_Data_From_Source(string source)
            {
                // Arrange

                // Act
                var actual = _objectToTest.HttpGetAsync(source, "data.json");

                // Assert
                actual.Should().NotBeNull();
            }

        }
    }
}
