using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularMonolithTemplate.Api.Common.Infrastructure.Persistance;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;

namespace ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance.Configuration;

[ExcludeFromCodeCoverage]
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
