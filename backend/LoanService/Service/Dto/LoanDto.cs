using LoanService.Entities;

namespace LoanService.Service.Dto;

public class LoanDto
{
    public required StatusType Status { get; set; }
    public required string Number { get; set; }
    public required decimal Amount { get; set; }
    public required int TermValue { get; set; }
    public required decimal InterestValue { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}