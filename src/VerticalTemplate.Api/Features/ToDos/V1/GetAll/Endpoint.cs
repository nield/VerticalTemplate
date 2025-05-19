namespace VerticalTemplate.Api.Features.ToDos.V1.GetAll;

internal sealed class Endpoint : EndpointWithoutRequest<IEnumerable<Response>, Mapper>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public override void Configure()
    {
        Get("ToDos");
        Version(1);
        AllowAnonymous();
        Description(x =>
            x.Produces<IEnumerable<Response>>()
            .WithGroupName(GroupConstants.ToDoGroupName));
        Summary(x => x.Description = "Used to get all ToDos");
    }

    public Endpoint(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _applicationDbContext.TodoItems
            .Select(x => Map.FromEntity(x))
            .ToListAsync(cancellationToken: ct);

        await SendAsync(response, cancellation: ct);
    }
}