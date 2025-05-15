namespace VerticalTemplate.Api.Features.ToDos.V1.GetToDo;

internal sealed class Response
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public List<string> Tags { get; set; } = [];
}
