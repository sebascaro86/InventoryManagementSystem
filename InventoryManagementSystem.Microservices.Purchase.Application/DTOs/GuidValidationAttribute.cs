using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs
{

    /// <summary>
    /// Represents a validation attribute for GUID values.
    /// </summary>
    public class GuidValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>A ValidationResult indicating whether the value is valid or not.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string guidString && !Guid.TryParse(guidString, out _))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
