using System.Net;
using VerticalTemplate.Api.Entities;
using VerticalTemplate.Api.Features.ToDos.V1.GetToDo;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.GetToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_InvalidId_Should_ReturnNotFound()
    {
        var id = 1L;

        _applicationDbContextMock.TodoItems.FindAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        var ep = Factory.Create<Endpoint>(ctx =>
        {
            ctx.Request.RouteValues.Add("id", id);
        }, _applicationDbContextMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal((int)HttpStatusCode.NotFound, ep.HttpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_ValidId_Should_ReturnData()
    {
        var id = 1L;

        var item = Builder<ToDoItem>.CreateNew().Build();

        _applicationDbContextMock.TodoItems.FindAsync(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var ep = Factory.Create<Endpoint>(ctx =>
        {
            ctx.Request.RouteValues.Add("id", id);
        }, _applicationDbContextMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal((int)HttpStatusCode.OK, ep.HttpContext.Response.StatusCode);
        Assert.Equal(item.Title, ep.Response.Title);
    }
}
