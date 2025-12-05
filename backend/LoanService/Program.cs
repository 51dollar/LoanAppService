using LoanService.Endpoints;
using LoanService.Middleware;
using Scalar.AspNetCore;
using LoanService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFrontendCors();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddAppHealthChecks(builder.Configuration);

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseFrontendCors();
app.ApplyMigrations();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapLoanEndpoints();
app.MapAppHealthChecks();

app.Run();