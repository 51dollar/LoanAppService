using System.Linq.Expressions;
using LoanService.Entities;
using LoanService.Service.Queries.Filter;
using LoanService.Service.Queries.Page;
using LoanService.Service.Queries.Sort;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Database.Extensions;

public static class LaonExtensions
{
    public static IQueryable<Loan> Filter(this IQueryable<Loan> query, LoanFilter filter)
    {
        if (filter.Status.HasValue)
            query = query.Where(x => x.Status == filter.Status);

        if (filter.MinAmount.HasValue)
            query = query.Where(x => x.Amount >= filter.MinAmount.Value);

        if (filter.MaxAmount.HasValue)
            query = query.Where(x => x.Amount <= filter.MaxAmount.Value);

        if (filter.MinTerm.HasValue)
            query = query.Where(x => x.TermValue >= filter.MinTerm.Value);

        if (filter.MaxTerm.HasValue)
            query = query.Where(x => x.TermValue <= filter.MaxTerm.Value);

        return query;
    }

    public static async Task<PageResult<Loan>> ToPageAsync(
        this IQueryable<Loan> query, PageParams pageParams, CancellationToken cancellationToken)
    {
        var totalCount  = await query.CountAsync(cancellationToken);
        if (totalCount  == 0)
            return new PageResult<Loan>([], 0);
        
        var page = pageParams.PageNumber ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return new PageResult<Loan>(result, pageSize);
    }

    public static IQueryable<Loan> Sort(this IQueryable<Loan> query, SortParams? sortParams)
    {
        if (sortParams == null || string.IsNullOrWhiteSpace(sortParams.OrderBy))
            return query;

        var selector = GetKeySelector(sortParams.OrderBy);
        if (selector == null)
            return query;

        return sortParams.SortDirection == SortDirection.Descending
            ? query.OrderByDescending(selector)
            : query.OrderBy(selector);
    }

    private static Expression<Func<Loan, object>>? GetKeySelector(string orderBy) =>
        orderBy switch
        {
            nameof(Loan.Status) => x => x.Status,
            nameof(Loan.Number) => x => x.Number,
            nameof(Loan.Amount) => x => x.Amount,
            nameof(Loan.TermValue) => x => x.TermValue,
            nameof(Loan.InterestValue) => x => x.InterestValue,
            nameof(Loan.CreatedAt) => x => x.CreatedAt,
            nameof(Loan.ModifiedAt) => x => x.ModifiedAt,
            _ => null
        };
}