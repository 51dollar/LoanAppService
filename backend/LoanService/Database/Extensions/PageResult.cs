namespace LoanService.Database.Extensions;

public class PageResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }

    public PageResult(IEnumerable<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }
}