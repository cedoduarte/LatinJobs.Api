using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Validators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PasswordsMatchAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty("Password");
            var passwordConfirmationProperty = validationContext.ObjectType.GetProperty("PasswordConfirmation");

            if (passwordProperty is null || passwordConfirmationProperty is null)
            {
                return new ValidationResult("Password and PasswordConfirmation properties are required.");
            }

            string? password = passwordProperty.GetValue(validationContext.ObjectInstance)?.ToString();
            string? passwordConfirmation = passwordConfirmationProperty.GetValue(validationContext.ObjectInstance)?.ToString();

            if (!string.Equals(password, passwordConfirmation, StringComparison.Ordinal))
            {
                return new ValidationResult("Password and confirmation do not match.");
            }
            return ValidationResult.Success;
        }
    }
}