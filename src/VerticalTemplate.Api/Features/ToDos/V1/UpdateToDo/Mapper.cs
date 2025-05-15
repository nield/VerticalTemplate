namespace VerticalTemplate.Api.Features.ToDos.V1.UpdateToDo;

internal sealed class Mapper : RequestMapper<Request, ToDoItem>
{
    public override ToDoItem UpdateEntity(Request r, ToDoItem e)
    {
        e.Title = r.Title;
        e.Tags = r.Tags;

        return e;
    }
}