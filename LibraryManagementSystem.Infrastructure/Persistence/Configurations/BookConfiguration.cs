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

        builder.Property(b => b.Title).HasMaxLength(150).IsRequired();
        builder.Property(b => b.Author).HasMaxLength(100).IsRequired();

        builder.OwnsOne(b => b.ISBN, isbn =>
        {
            isbn.Property(i => i.Value)
                .IsRequired();
        });

        builder.Property(b => b.PublicationYear).IsRequired();
        builder.Property(b => b.Status)
               .HasConversion(
            status => status.ToString(),
            status => (Status)Enum.Parse(typeof(Status), status));

    }
}
