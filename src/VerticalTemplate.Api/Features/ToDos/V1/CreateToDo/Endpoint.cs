namespace VerticalTemplate.Api.Features.ToDos.V1.CreateToDo;

internal class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public Endpoint(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public override void Configure()
    {
        Post("ToDos");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces<Response>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to create a ToDo");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = Map.ToEntity(req);

        _applicationDbContext.TodoItems.Add(entity);

        await _applicationDbContext.SaveChangesAsync(ct);

        var response = Map.FromEntity(entity);

        await SendCreatedAtAsync<GetToDo.Endpoint>(new { id = response.Id }, response, cancellation: ct);
    }
}