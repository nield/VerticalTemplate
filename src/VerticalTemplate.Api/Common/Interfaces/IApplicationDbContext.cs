using Microsoft.EntityFrameworkCore;

namespace VerticalTemplate.Api.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ToDoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}