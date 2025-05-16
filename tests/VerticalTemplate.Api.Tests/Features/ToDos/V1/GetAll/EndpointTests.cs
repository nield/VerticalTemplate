using VerticalTemplate.Api.Entities;
using VerticalTemplate.Api.Features.ToDos.V1.GetAll;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.GetAll;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_Data_Exists_Should_ReturnData()
    {
        var items = Builder<ToDoItem>.CreateListOfSize(1)
            .Build().AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.TodoItems
            .Returns(items);

        var ep = Factory.Create<Endpoint>(_applicationDbContextMock);

        await ep.HandleAsync(CancellationToken.None);

        Assert.Equal(StatusCodes.Status200OK, ep.HttpContext.Response.StatusCode);
        Assert.NotEmpty(ep.Response);
    }
}
