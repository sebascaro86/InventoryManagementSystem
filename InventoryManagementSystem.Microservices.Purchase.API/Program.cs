using InventoryManagementSystem.Infrastructure.loC;
using InventoryManagementSystem.Microservices.Purchase.API.Middlewares;
using InventoryManagementSystem.Microservices.Purchase.API.Models;
using Microsoft.AspNetCore.Mvc;
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
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.RegisterServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

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

app.UseRouting();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseExceptionMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
