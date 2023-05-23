using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NearMessage.Domain.Entities;

namespace NearMessage.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        /*builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id).IsUnique();

        builder.HasIndex(user => user.UserName).IsUnique();
        builder.Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(user => user.CreatedAt).IsRequired();*/
    }
}
