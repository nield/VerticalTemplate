﻿namespace VerticalTemplate.Api.Features.ToDos.V1.DeleteToDo;

internal sealed class Endpoint : EndpointWithoutRequest<NoContent>
{
    private readonly IToDoRepository _toDoRepository;

    public override void Configure()
    {
        Delete("ToDos/{id}");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to delete a ToDo");
    }

    public Endpoint(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<long>("id");

        var entity = await _toDoRepository.GetByIdAsync(id, ct);

        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _toDoRepository.DeleteAsync(entity, ct);

        await SendNoContentAsync(ct);
    }
}