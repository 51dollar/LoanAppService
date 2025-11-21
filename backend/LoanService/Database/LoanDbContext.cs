using LoanService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Database;

public class LoanDbContext(DbContextOptions<LoanDbContext>  options) : DbContext(options)
{
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(x => x.Id);
            
            entity.Property(x => x.Number)
                .IsRequired();
            entity.Property(x => x.Amount)
                .HasColumnType("numeric(18,3)")
                .IsRequired();
            entity.Property(x => x.TermValue)
                .IsRequired();
            entity.Property(x => x.InterestValue)
                .HasColumnType("numeric(5,2)")
                .IsRequired();
            entity.Property(x => x.CreatedAt)
                .IsRequired();
            
            entity.HasIndex(x => x.Number)
                .IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}