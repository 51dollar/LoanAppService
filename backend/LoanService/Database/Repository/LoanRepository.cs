using LoanService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Database.Repository;

internal class LoanRepository(LoanDbContext context) : ILoanRepository
{
    public async Task<IEnumerable<Loan?>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<Loan>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Loan?> GetByNumberAsync(string number, CancellationToken cancellationToken)
    {
        return await context.Set<Loan>()
            .FirstOrDefaultAsync(x => x.Number == number, cancellationToken);
    }
    
    public async Task CreateAsync(Loan entity, CancellationToken cancellationToken)
    {
        await context.Set<Loan>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Loan entity, CancellationToken cancellationToken)
    {
        context.Set<Loan>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string numberLoan, CancellationToken cancellationToken)
    {
        var entity = await context.Set<Loan>()
            .FirstOrDefaultAsync(x => x.Number == numberLoan, cancellationToken);
        
        if (entity == null)
            throw new KeyNotFoundException($"Loan with number '{numberLoan}' not found.");

        context.Set<Loan>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}