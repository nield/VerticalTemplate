using VerticalTemplate.Api.Features.ToDos.V1.DeleteAll;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.DeleteAll;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_DeleteAll_Success_Should_ReturnNoContent()
    {
        var ep = Factory.Create<Endpoint>(_toDoRepositoryMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal(StatusCodes.Status204NoContent, ep.HttpContext.Response.StatusCode);
    }
}
