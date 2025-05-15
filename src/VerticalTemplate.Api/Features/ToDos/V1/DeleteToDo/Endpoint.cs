using Microsoft.AspNetCore.Http.HttpResults;

namespace VerticalTemplate.Api.Features.ToDos.V1.DeleteToDo;

internal sealed class Endpoint : EndpointWithoutRequest<NoContent>
{
    private readonly IApplicationDbContext _applicationDbContext;

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

        _applicationDbContext.TodoItems.Remove(entity);

        await _applicationDbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}