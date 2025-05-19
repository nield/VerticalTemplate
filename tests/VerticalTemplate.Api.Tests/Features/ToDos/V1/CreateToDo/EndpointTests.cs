using Microsoft.Extensions.DependencyInjection;
using VerticalTemplate.Api.Features.ToDos.V1.CreateToDo;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.CreateToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Handle_Success()
    {
        var request = Builder<Request>.CreateNew().Build();

        var ep = Factory.Create<Endpoint>(ctx =>
        {
            ctx.AddTestServices(s => s.AddSingleton(_linkGeneratorMock));
        }, _toDoRepositoryMock);

        await ep.HandleAsync(request, CancellationToken.None);

        Assert.Equal(StatusCodes.Status201Created, ep.HttpContext.Response.StatusCode);
        Assert.NotNull(ep.Response);        
    }
}
