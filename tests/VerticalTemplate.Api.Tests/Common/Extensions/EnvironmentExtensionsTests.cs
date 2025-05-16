using Microsoft.Extensions.Hosting;
using VerticalTemplate.Api.Common.Constants;

namespace VerticalTemplate.Api.Tests.Common.Extensions;

public class EnvironmentExtensionsTests
{
    private readonly IHostEnvironment _environmentMock = Substitute.For<IHostEnvironment>();

    [Fact]
    public void Given_EnvironmentIsTest_When_CheckingIsTest_Then_IsTrue()
    {
        _environmentMock.EnvironmentName
            .Returns(EnvironmentConstants.Test);

        var sut = _environmentMock.IsTest();

        Assert.True(sut);
    }
}