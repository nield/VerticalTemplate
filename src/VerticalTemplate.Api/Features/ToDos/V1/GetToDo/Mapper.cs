namespace VerticalTemplate.Api.Features.ToDos.V1.GetToDo;

internal sealed class Mapper : ResponseMapper<Response, ToDoItem>
{
    public override Response FromEntity(ToDoItem e)
    {
        return new Response
        {
            Id = e.Id,
            Title = e.Title,
            Tags = e.Tags
        };
    }
}