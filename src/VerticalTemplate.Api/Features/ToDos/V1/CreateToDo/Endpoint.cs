namespace VerticalTemplate.Api.Features.ToDos.V1.CreateToDo;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IToDoRepository _toDoRepository;

    public Endpoint(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public override void Configure()
    {
        Post("ToDos");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces<Response>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to create a ToDo");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = Map.ToEntity(req);

        await _toDoRepository.AddAsync(entity, ct);

        var response = Map.FromEntity(entity);

        await SendCreatedAtAsync<GetToDo.Endpoint>(new { id = response.Id }, response, cancellation: ct);
    }
}