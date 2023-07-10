using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Infrastructure.loC;
using InventoryManagementSystem.Microservices.Inventory.API.Middlewares;
using InventoryManagementSystem.Microservices.Inventory.API.Models;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using InventoryManagementSystem.Microservices.Inventory.Application.Services;
using InventoryManagementSystem.Microservices.Inventory.Domain.EventHandlers;
using InventoryManagementSystem.Microservices.Inventory.Domain.Events;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;
using InventoryManagementSystem.Microservices.Inventory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return new BadRequestObjectResult(new ErrorResponse
            {
                Message = "Model validation error",
                Errors = errors
            });
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register services and repository
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IEventHandler<UpdatedProductsEvent>, UpdateProductsEventHandler>();
builder.Services.AddTransient<UpdateProductsEventHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<UpdatedProductsEvent, UpdateProductsEventHandler>();

// Create database migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InventoryDBContext>();
    context.Database.Migrate();
}

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

app.UseRouting();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseExceptionMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
