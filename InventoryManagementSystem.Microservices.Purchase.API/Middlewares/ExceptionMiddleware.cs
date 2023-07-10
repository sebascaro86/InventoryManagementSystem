using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Microservices.Purchase.API.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace InventoryManagementSystem.Microservices.Purchase.API.Middlewares
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
        /// Invokes the middleware asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, logger, ex, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, logger, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, logger, ex, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Handles exceptions and writes the error response to the HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="ex">The exception.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task HandleExceptionAsync(HttpContext context, ILogger<ExceptionMiddleware> logger, Exception ex, HttpStatusCode statusCode)
        {
            logger.LogError(ex, "An error occurred in the PurchaseAPI");

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Message = statusCode == HttpStatusCode.InternalServerError ? "An error occurred in the PurchaseAPI" : ex.Message
            };

            var json = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }

    /// <summary>
    /// Extension methods for adding the <see cref="ExceptionMiddleware"/> to the application pipeline.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Adds the <see cref="ExceptionMiddleware"/> to the application pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The updated application builder.</returns>
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
