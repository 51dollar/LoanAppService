namespace LoanService.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public StatusType Status { get; set; }
    public string Number { get; set; }
    public decimal Amount { get; set; }
    public int TermValue { get; set; }
    public decimal InterestValue { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}