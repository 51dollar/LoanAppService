using LoanService.Service.Dto;

namespace LoanService.Service;

public interface ILoanService
{
    Task<IEnumerable<LoanDto>> GetAllLoansAsync(CancellationToken cancellationToken = default);
    Task<LoanDto> CreateLoanAsync(LoanCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(string numberLoan, LoanUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(string numberLoan, CancellationToken cancellationToken = default);
}