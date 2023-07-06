using InventoryManagementSystem.Infrastructure.loC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opt =>
    {
        opt.PreSerializeFilters.Add((swagger, request) =>
        {
            var serverUrl = $"{request.Headers["X-Forwarded-Proto"]}://" +
             $"{request.Headers["X-Forwarded-Host"]}/" +
             $"{request.Headers["X-Forwarded-Prefix"]}";

            swagger.Servers = new List<OpenApiServer> { new() { Url = serverUrl } };
        });
    });
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
