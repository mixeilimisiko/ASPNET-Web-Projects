using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.Persistence.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x  => x.Id);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).IsUnicode(false)
                                             .IsRequired()
                                             .HasMaxLength(128);

            builder.Property(x => x.PasswordHash).IsUnicode(false)
                                                 .IsRequired();
            
            builder.Property(x => x.CreatedAt).HasColumnType("datetime")
                                              .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.ModifiedAt).HasColumnType("datetime")
                                               .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Status).IsRequired().HasDefaultValue(Statuses.Active);

            builder.HasMany(x => x.ToDos).WithOne(x => x.User);



        }
    }
}
