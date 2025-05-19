namespace VerticalTemplate.Api.Features.ToDos.V1.DeleteAll;

internal sealed class Endpoint : EndpointWithoutRequest<NoContent>
{
    private readonly IToDoRepository _toDoRepository;

    public override void Configure()
    {
        Delete("ToDos");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces(StatusCodes.Status204NoContent)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to delete all ToDos");
    }

    public Endpoint(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _toDoRepository.DeleteAll(ct);

        await SendNoContentAsync(ct);
    }
}