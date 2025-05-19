using System.Net.Http.Json;

namespace VerticalTemplate.Api.Integration.Tests.Features.ToDos.V2;

[Collection("WebApplicationCollection")]
public class ToDoTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public ToDoTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task GetAll_Should_ReturnData()
    {
        var sut = await _webApplicationFixture.HttpClient.GetFromJsonAsync<IEnumerable<Api.Features.ToDos.V2.GetAll.Response>>(
            "/api/v2/todos");

        Assert.NotNull(sut);
        Assert.NotEmpty(sut);
    }
}
