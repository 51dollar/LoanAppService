using LoanService.Entities;

namespace LoanService.Database.Repository;

public interface ILoanRepository
{
    Task<IEnumerable<Loan?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Loan?> GetByNumberAsync(string number, CancellationToken cancellationToken);
    Task CreateAsync(Loan entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Loan entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(string numberLoan, CancellationToken cancellationToken = default);
}