using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

internal class LoanConfiguration : IEntityTypeConfiguration<Borrowing>
{
    public void Configure(EntityTypeBuilder<Borrowing> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.UserId);
        builder.Property(l => l.BookId);
        builder.Property(l => l.CheckoutDate);
        builder.Property(l => l.ReturnDate);

        builder.HasOne(l => l.User).WithMany(u => u.LoanList).HasForeignKey(l => l.UserId);
        builder.HasOne(l => l.Book).WithMany(b => b.LoanList).HasForeignKey(l => l.BookId);
    }
}
