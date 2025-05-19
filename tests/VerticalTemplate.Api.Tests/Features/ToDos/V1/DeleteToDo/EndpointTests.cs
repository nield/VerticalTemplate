using VerticalTemplate.Api.Entities;
using VerticalTemplate.Api.Features.ToDos.V1.DeleteToDo;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.DeleteToDo;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_InvalidId_Should_ReturnNotFound()
    {
        var id = 1L;

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        var ep = Factory.Create<Endpoint>(ctx =>
        {
            ctx.Request.RouteValues.Add("id", id);
        }, _toDoRepositoryMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal(StatusCodes.Status404NotFound, ep.HttpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_ValidId_Should_ReturnNoContent()
    {
        var id = 1L;

        var item = Builder<ToDoItem>.CreateNew().Build();

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var ep = Factory.Create<Endpoint>(ctx =>
        {
            ctx.Request.RouteValues.Add("id", id);
        }, _toDoRepositoryMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal(StatusCodes.Status204NoContent, ep.HttpContext.Response.StatusCode);
    }
}
