namespace VerticalTemplate.Api.Features.ToDos.V1.UpdateToDo;

internal sealed class Endpoint : Endpoint<Request, NoContent, Mapper>
{
    private readonly IToDoRepository _toDoRepository;

    public override void Configure()
    {
        Put("ToDos/{id}");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to update a ToDo");
    }

    public Endpoint(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var id = Route<long>("id");

        var entity = await _toDoRepository.GetByIdAsync(id, ct);

        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Map.UpdateEntity(r, entity);

        await _toDoRepository.UpdateAsync(entity, ct);

        await SendNoContentAsync(ct);
    }
}