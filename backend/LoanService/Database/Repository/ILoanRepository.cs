using LoanService.Database.Extensions;
using LoanService.Entities;
using LoanService.Service.Queries.Filter;
using LoanService.Service.Queries.Page;
using LoanService.Service.Queries.Sort;

namespace LoanService.Database.Repository;

public interface ILoanRepository
{
    Task<PageResult<Loan>> GetAllAsync(
        LoanFilter filter, SortParams sortParams, PageParams pageParams, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNumberAsync(string number, CancellationToken cancellationToken  = default);
    Task<Loan?> GetByNumberAsync(string number, CancellationToken cancellationToken);
    Task CreateAsync(Loan entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Loan entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(string numberLoan, CancellationToken cancellationToken = default);
}