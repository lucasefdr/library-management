using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations;

internal class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
{
    public void Configure(EntityTypeBuilder<Borrowing> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id);
        builder.Property(b => b.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.BookId).IsRequired();
        builder.Property(b => b.CheckoutDate).HasColumnType("datetime").IsRequired();
        builder.Property(b => b.DueDate).HasColumnType("datetime").IsRequired();
        builder.Property(b => b.ReturnDate).HasColumnType("datetime");

        builder.HasOne(b => b.User).WithMany(u => u.LoanList).HasForeignKey(b => b.UserId);
        builder.HasOne(b => b.Book).WithMany(b => b.LoanList).HasForeignKey(b => b.BookId);
    }
}