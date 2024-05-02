

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.Persistence.Config
{
    public class ToDoConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Title);

            builder.HasIndex(x => new { x.Title, x.UserId }).IsUnique();

            builder.Property(x => x.Title).IsUnicode(false).HasMaxLength(100).IsRequired();

            builder.Property(x => x.TargetDate).HasColumnType("datetime");

            builder.Property(x => x.Status).HasDefaultValue(Statuses.Active);

            builder.Property(x => x.CreatedAt).HasColumnType("datetime")
                                              .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.ModifiedAt).HasColumnType("datetime")
                                               .HasDefaultValue(DateTime.Now);

            builder.HasMany(x => x.Subtasks).WithOne(x => x.Todo).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
