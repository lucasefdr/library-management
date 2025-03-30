using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id);
        builder.Property(u => u.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(128);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                 .HasMaxLength(128)
                 .IsRequired();
        });
    }
}
