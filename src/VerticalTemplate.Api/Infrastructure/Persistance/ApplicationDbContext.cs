using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace VerticalTemplate.Api.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    #region DbSets

    public DbSet<ToDoItem> TodoItems => Set<ToDoItem>();

    #endregion

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
