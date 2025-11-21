using LoanService.Service;
using LoanService.Service.Dto;

namespace LoanService.Endpoints;

public static class LoanEndpoints
{
    public static IEndpointRouteBuilder MapLoanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loans");

        group.MapGet("", GetAllLoansAsync);
        group.MapPost("", CreateLoanAsync);
        group.MapPatch("/{number}", UpdateLoanAsync);
        group.MapDelete("/{number}", DeleteLoanAsync);

        return app;
    }
    
    private static async Task<IResult> GetAllLoansAsync(ILoanService service, CancellationToken ct)
    {
        var result = await service.GetAllLoansAsync(ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> CreateLoanAsync(LoanCreateDto? dto, ILoanService service, CancellationToken ct)
    {
        if (dto is null)
            return Results.BadRequest(new { error = "Request body is required." });
        
        try
        {
            var loanDto = await service.CreateLoanAsync(dto, ct);
            return Results.Created($"/api/loans/{loanDto.Number}", loanDto);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateLoanAsync(string number, LoanUpdateDto? dto, ILoanService service, CancellationToken ct)
    {
        if (dto is null)
            return Results.BadRequest(new { error = "Request body is required." });
        
        try
        {
            await service.UpdateAsync(number, dto, ct);
            return Results.NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }

    private static async Task<IResult> DeleteLoanAsync(string number, ILoanService service, CancellationToken ct)
    {
        try
        {
            await service.DeleteAsync(number, ct);
            return Results.NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }
}