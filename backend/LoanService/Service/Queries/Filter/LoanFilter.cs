using LoanService.Entities;

namespace LoanService.Service.Queries.Filter;

public class LoanFilter
{
    public StatusType? Status { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public int? MinTerm { get; set; }
    public int? MaxTerm { get; set; }
}