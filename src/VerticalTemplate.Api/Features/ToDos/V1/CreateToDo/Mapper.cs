namespace VerticalTemplate.Api.Features.ToDos.V1.CreateToDo;

internal sealed class Mapper : Mapper<Request, Response, ToDoItem>
{
    public override ToDoItem ToEntity(Request r)
    {
        return new ToDoItem
        {
            Title = r.Title,
            Tags = r.Tags
        };
    }

    public override Response FromEntity(ToDoItem e)
    {
        return new Response
        {
            Id = e.Id
        };
    }
}