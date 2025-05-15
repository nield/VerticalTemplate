using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VerticalTemplate.Api.Infrastructure.Persistance.Configuration;

public class ToDoConfiguration : BaseConfiguration<ToDoItem>
{
    public override string TableName => "ToDo";

    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(1000)
            .IsUnicode(false)
            .IsRequired(false);
    }
}
