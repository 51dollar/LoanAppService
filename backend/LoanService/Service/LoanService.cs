using LoanService.Database.Repository;
using LoanService.Entities;
using LoanService.Service.Dto;

namespace LoanService.Service;

internal class LoanService(ILoanRepository repository) : ILoanService
{
    public async Task<IEnumerable<LoanDto>> GetAllLoansAsync(CancellationToken cancellationToken)
    {
        var entity = await repository.GetAllAsync(cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException("Not found.");

        var loansDto = entity.Select(x => new LoanDto
        {
            Status =  x.Status = StatusType.Published,
            Number = x.Number.ToLower(),
            Amount = x.Amount,
            TermValue = x.TermValue,
            InterestValue = x.InterestValue,
            CreatedAt = x.CreatedAt,
        });
        
        return loansDto;
    }

    public async Task<LoanDto> CreateLoanAsync(LoanCreateDto createDto, CancellationToken cancellationToken)
    {
        if (createDto.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0");

        if (createDto.TermValue <= 0)
            throw new ArgumentException("TermValue must be greater than 0");

        if (createDto.InterestValue <= 0)
            throw new ArgumentException("InterestValue must be greater than 0");
        
        var entity = new Loan
        {
            Status = StatusType.Published,
            Number =  createDto.Number,
            Amount = createDto.Amount,
            TermValue = createDto.TermValue,
            InterestValue = createDto.InterestValue,
            CreatedAt = DateTimeOffset.UtcNow,
            ModifiedAt = DateTimeOffset.UtcNow
        };
        
        await repository.CreateAsync(entity, cancellationToken);

        var dto = new LoanDto
        {
            Status = entity.Status,
            Number = entity.Number,
            Amount = entity.Amount,
            TermValue = entity.TermValue,
            InterestValue = entity.InterestValue,
            CreatedAt = entity.CreatedAt
        };
        
        return dto;
    }

    public async Task UpdateAsync(string number, LoanUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByNumberAsync(number, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Loan with number '{number}' not found.");

        if (dto.Amount is > 0)
            entity.Amount = dto.Amount.Value;

        if (dto.TermValue is > 0)
            entity.TermValue = dto.TermValue.Value;

        if (dto.InterestValue is > 0)
            entity.InterestValue = dto.InterestValue.Value;

        if (dto.Status.HasValue)
            entity.Status = dto.Status.Value;

        entity.ModifiedAt = DateTimeOffset.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(string numberLoan, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(numberLoan, cancellationToken);
    }
}