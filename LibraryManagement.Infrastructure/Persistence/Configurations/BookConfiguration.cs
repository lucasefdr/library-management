using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id);
        builder.Property(b => b.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(b => b.Title).HasMaxLength(128).IsRequired();
        builder.Property(b => b.Author).HasMaxLength(128).IsRequired();
        builder.Property(b => b.PublicationYear).HasColumnType("int").IsRequired();

        builder.OwnsOne(b => b.ISBN, isbn =>
        {
            isbn.Property(i => i.Value)
                .HasMaxLength(13)
                .IsRequired();
        });

        builder.Property(b => b.Status)
               .HasConversion(
            status => status.ToString(),
            status => (Status)Enum.Parse(typeof(Status), status)).HasMaxLength(64).IsRequired();

    }
}
