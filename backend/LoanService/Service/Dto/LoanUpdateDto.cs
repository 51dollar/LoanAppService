using LoanService.Entities;

namespace LoanService.Service.Dto;

public class LoanUpdateDto
{
    public StatusType? Status { get; set; }
    public decimal? Amount { get; set; }
    public int? TermValue { get; set; }
    public decimal? InterestValue { get; set; }
}