using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.CleanArchitecture.WebApi.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.UseWebApiExceptionHandler();
app.Run();
