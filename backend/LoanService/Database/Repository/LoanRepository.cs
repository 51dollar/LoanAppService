using LoanService.Database.Extensions;
using LoanService.Entities;
using LoanService.Service.Queries.Filter;
using LoanService.Service.Queries.Page;
using LoanService.Service.Queries.Sort;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Database.Repository;

internal class LoanRepository(LoanDbContext context) : ILoanRepository
{
    public async Task<PageResult<Loan>> GetAllAsync(
        LoanFilter filter,
        SortParams sortParams,
        PageParams pageParams,
        CancellationToken cancellationToken)
    {
        return await context.Set<Loan>()
            .AsNoTracking()
            .Filter(filter)
            .Sort(sortParams)
            .ToPageAsync(pageParams, cancellationToken);
    }
    
    public async Task<bool> ExistsByNumberAsync(string number, CancellationToken cancellationToken)
    {
        return await context.Loans.AnyAsync(x => x.Number == number, cancellationToken);
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