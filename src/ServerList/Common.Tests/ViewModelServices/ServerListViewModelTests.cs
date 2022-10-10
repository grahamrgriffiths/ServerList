using AutoFixture.Xunit2;
using Common.Http;
using Core.Models;
using FluentAssertions;
using Moq;
using ServerList.ViewModelServices;
using Xunit;

namespace ServerList.Tests.ViewModelServices
{
    public class ServerListServiceTests
    {
        private readonly Mock<IHttpWrapper> _mockHttpWrapper;
        private readonly Mock<IServerListService> _mockServerListService;
        private readonly ServerListService _objectToTest;
        public ServerListServiceTests()
        {
            _mockHttpWrapper = new Mock<IHttpWrapper>();
            _mockServerListService = new Mock<IServerListService>();
            _objectToTest = new ServerListService(_mockHttpWrapper.Object);
        }

        public class GetLocationDataTests : ServerListServiceTests
        {
            private void SetupGetLocationDataTests()
            {
                var mockLocationResponse = "{\"Code\":1000,\"IP\":\"1.1.1.1\",\"Lat\":35.48660,\"Long\":-6.2549,\"Country\":\"GB\",\"ISP\":\"Test ISP\"}";
                _mockHttpWrapper
                    .Setup(x => x.HttpGetAsync(It.IsAny<string>(), It.IsAny<string>(), null))
                    .ReturnsAsync(mockLocationResponse);
            }

            [Fact]
            public async Task Gets_LocationData_From_Source()
            {
                // Arrange
                var expected = new LocationResponse
                {
                    Code = 1000,
                    Lat = 35.48660,
                    Long = -6.2549,
                    Country = "GB"
                };
                
                SetupGetLocationDataTests();

                // Act
                var actual = await _objectToTest.GetLocationData();

                // Assert
                _mockHttpWrapper.Verify(x => x.HttpGetAsync(It.IsAny<string>(), It.IsAny<string>(), null), Times.Once);
                actual.Should().BeEquivalentTo(expected);
            }
        }

        public class GetLogicalServersTests : ServerListServiceTests
        {
            private void SetupGetLogicalServersTests(LocationResponse mockLocation)
            {
                var mockLogicalsResponse = @"{
                    ""Code"": 1000,
                    ""LogicalServers"": [{
                            ""Name"": ""IS#1"",
                            ""EntryCountry"": ""IS"",
                            ""ExitCountry"": ""IS"",
                            ""Domain"": ""node-is-01.protonvpn.net"",
                            ""Tier"": 2,
                            ""Features"": 4,
                            ""Region"": null,
                            ""City"": ""Reykjavik"",
                            ""Score"": 1000.50814427,
                            ""HostCountry"": null,
                            ""ID"": ""OYB-3pMQQA2Z2Qnp5s5nIvTVO2alU6h82EGLXYHn1mpbsRvE7UfyAHbt0_EilRjxhx9DCAUM9uXfM2ZUFjzPXw=="",
                            ""Location"": {
                                ""Lat"": 64.129999999999995,
                                ""Long"": -21.93
                            },
                            ""Status"": 1,
                            ""Servers"": [{
                                    ""EntryIP"": ""185.159.158.1"",
                                    ""ExitIP"": ""185.159.158.100"",
                                    ""Domain"": ""node-is-01.protonvpn.net"",
                                    ""ID"": ""OYB-3pMQQA2Z2Qnp5s5nIvTVO2alU6h82EGLXYHn1mpbsRvE7UfyAHbt0_EilRjxhx9DCAUM9uXfM2ZUFjzPXw=="",
                                    ""Label"": ""2"",
                                    ""X25519PublicKey"": ""yKbYe2XwbeNN9CuPZcwMF/lJp6a62NEGiHCCfpfxrnE="",
                                    ""Generation"": 0,
                                    ""Status"": 1,
                                    ""ServicesDown"": 0,
                                    ""ServicesDownReason"": null
                                }
                            ],
                            ""Load"": 100
                        }
                    ]
                }";

                _mockHttpWrapper
                    .Setup(x => x.HttpGetAsync(It.IsAny<string>(), It.IsAny<string>(), null))
                    .ReturnsAsync(mockLogicalsResponse);

                _mockServerListService.Setup(x => x.GetLogicalServers(mockLocation))
                    .ReturnsAsync(new List<LogicalServer>() {
                    new LogicalServer { Location = mockLocation } }
                );

            }

            [Theory, AutoData]
            public async Task Gets_LocationData_From_Source(LocationResponse mockLocation)
            {
                // Arrange
                SetupGetLogicalServersTests(mockLocation);
                
                // Act
                var actual = await _mockServerListService.Object.GetLogicalServers(mockLocation);

                // Assert
                _mockServerListService.Verify(x => x.GetLogicalServers(mockLocation), Times.Once);
                actual.Should().HaveCountGreaterThan(0);
            }
        }
    }
}
