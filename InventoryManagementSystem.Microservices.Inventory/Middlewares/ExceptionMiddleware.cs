using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Microservices.Inventory.API.Models;
using Newtonsoft.Json;
using System.Net;

namespace InventoryManagementSystem.Microservices.Inventory.API.Middlewares
{
    /// <summary>
    /// Represents a middleware for handling exceptions in the RealEstateAPI.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The RequestDelegate.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware asynchronously to handle exceptions in the request pipeline.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                logger.LogInformation("Action execution started: {Method} - {ActionName}", context.Request.Method,
                    context.Request.Path.Value?.ToLower());

                await _next(context);

                logger.LogInformation("Action execution completed:  {Method} - {ActionName}", context.Request.Method,
                    context.Request.Path.Value?.ToLower());
            }
            catch (NotFoundException ex)
            {
                logger.LogInformation(ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    Message = ex.Message
                };

                var json = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occurred in the RealEstateAPI");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    Message = "An error occurred in the RealEstateAPI"
                };

                var json = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
