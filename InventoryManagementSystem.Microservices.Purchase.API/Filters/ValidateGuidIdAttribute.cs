using InventoryManagementSystem.Microservices.Purchase.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Filters
{
    public class ValidateGuidIdAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateGuidIdAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The name of the GUID parameter.</param>
        public ValidateGuidIdAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        /// <summary>
        /// Validates the GUID parameter before executing the action.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        /// <param name="next">The delegate representing the next action execution in the pipeline.</param>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!Guid.TryParse(context.ActionArguments[_parameterName]?.ToString(), out Guid id))
            {
                var response = new ErrorResponse
                {
                    Message = "Model validation error",
                    Errors = new List<string> { $"Invalid {_parameterName} Guid Format" }
                };

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
