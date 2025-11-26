using LoanService.Database.Extensions;
using LoanService.Endpoints;
using LoanService.Middleware;
using LoanService.Service;
using Scalar.AspNetCore;
using LoanService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFrontendCors();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseGlobalExceptionHandler();
app.MapLoanEndpoints();

app.UseHttpsRedirection();
app.UseFrontendCors();

app.MapGet("/", () => "Hello World!");

app.Run();