using LoanService.Entities;
using LoanService.Service.Dto;
using LoanService.Service.Queries.Filter;
using LoanService.Service.Queries.Page;
using LoanService.Service.Queries.Sort;

namespace LoanService.Service;

public interface ILoanService
{
    Task<PageResult<LoanDto>> GetAllLoansAsync(
        LoanFilter filter, SortParams sortParams, PageParams pageParams, CancellationToken cancellationToken = default);
    Task<LoanDto> CreateLoanAsync(LoanCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(string numberLoan, LoanUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(string numberLoan, CancellationToken cancellationToken = default);
}