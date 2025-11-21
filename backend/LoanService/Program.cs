using LoanService.Database;
using LoanService.Endpoints;
using LoanService.Middleware;
using LoanService.Service;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
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
app.UseCors();

app.MapGet("/", () => "Hello World!");

app.Run();