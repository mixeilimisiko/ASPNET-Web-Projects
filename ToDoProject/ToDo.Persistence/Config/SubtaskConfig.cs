
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.Persistence.Config
{
    public class SubtaskConfig : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(100)
                                          .IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnType("datetime")
                                              .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.ModifiedAt).HasColumnType("datetime")
                                               .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Status).IsRequired()
                                           .HasDefaultValue(Statuses.Active);

            builder.Property(x => x.ToDoId).IsRequired();

            //builder.HasOne(x => x.Todo).WithMany(x => x.Subtasks);
        }
    }
}
