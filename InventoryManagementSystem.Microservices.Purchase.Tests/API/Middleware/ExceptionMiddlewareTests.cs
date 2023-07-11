using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Microservices.Purchase.API.Middlewares;
using InventoryManagementSystem.Microservices.Purchase.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.API.Middleware
{
    /// <summary>
    /// Unit tests for the <see cref="ExceptionMiddlewareTests"/> class.
    /// </summary>
    [TestFixture]
    public class ExceptionMiddlewareTests
    {
        private Mock<ILogger<ExceptionMiddleware>> _loggerMock;
        private ExceptionMiddleware _middleware;
        private RequestDelegate _next;
        private HttpContext _httpContext;

        /// <summary>
        /// Initial setup that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            _httpContext = new DefaultHttpContext();
        }

        /// <summary>
        /// Tests the <see cref="ExceptionMiddleware.InvokeAsync"/> method when a <see cref="NotFoundException"/> is thrown.
        /// It should return a NotFound response with the corresponding error message.
        /// </summary>
        [Test]
        public async Task InvokeAsync_WithNotFoundException_ReturnsNotFoundResponse()
        {
            // Arrange
            var errorMessage = "Not found.";
            _next = context => throw new NotFoundException(errorMessage);
            _middleware = new ExceptionMiddleware(_next);
            _httpContext.Response.Body = new MemoryStream();

            // Act
            await _middleware.InvokeAsync(_httpContext, _loggerMock.Object);

            // Assert
            _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(_httpContext.Response.Body).ReadToEndAsync();
            var responseJson = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(responseJson.Message, Is.EqualTo(errorMessage));
        }

        /// <summary>
        /// Tests the <see cref="ExceptionMiddleware.InvokeAsync"/> method when a general <see cref="Exception"/> is thrown.
        /// It should return an InternalServerError response with a generic error message.
        /// </summary>
        [Test]
        public async Task InvokeAsync_WithGeneralException_ReturnsInternalServerErrorResponse()
        {
            // Arrange
            var errorMessage = "An error occurred.";
            _next = context => throw new Exception(errorMessage);
            _middleware = new ExceptionMiddleware(_next);
            _httpContext.Response.Body = new MemoryStream();

            // Act
            await _middleware.InvokeAsync(_httpContext, _loggerMock.Object);

            // Assert
            _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(_httpContext.Response.Body).ReadToEndAsync();
            var responseJson = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
            Assert.That(responseJson.Message, Is.EqualTo("An error occurred in the PurchaseAPI"));
        }
    }
}
