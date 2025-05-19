namespace VerticalTemplate.Api.Features.ToDos.V1.GetToDo;

internal sealed class Endpoint : EndpointWithoutRequest<Response, Mapper>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public override void Configure()
    {
        Get("ToDos/{id}");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces<Response>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to get a ToDo");
    }

    public Endpoint(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<long>("id");

        var entity = await _applicationDbContext.TodoItems.FindAsync(id, ct);

        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = Map.FromEntity(entity);

        await SendOkAsync(response, ct);
    }
}