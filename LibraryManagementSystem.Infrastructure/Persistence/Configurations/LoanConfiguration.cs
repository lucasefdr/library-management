using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

internal class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasOne(l => l.User).WithMany(u => u.LoanList).HasForeignKey(l => l.UserId);
        builder.HasOne(l => l.Book).WithMany(b => b.LoanList).HasForeignKey(l => l.BookId);
    }
}
