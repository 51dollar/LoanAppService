namespace LoanService.Service.Dto;

public class LoanCreateDto
{
    public string Number { get; set; }
    public decimal Amount { get; set; }
    public int TermValue { get; set; }
    public decimal InterestValue { get; set; }
}