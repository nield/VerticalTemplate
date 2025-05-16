using Types = NetArchTest.Rules.Types;

namespace VerticalTemplate.Api.Tests;

public class ArchTests
{
    [Fact]
    public void Features_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalTemplate.Api.Features")
            .ShouldNot()
            .HaveDependencyOn("VerticalTemplate.Api.Infrastructure")
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }

    [Fact]
    public void Features_ShouldBeSealedAndInternal()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalTemplate.Api.Features")
            .Should()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
}
