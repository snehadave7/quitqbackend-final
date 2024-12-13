using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CaseStudyQuitQ.Validations {
    public class PasswordValidation:ValidationAttribute {

        public const string message = "Password should contain atleast 1 uppercase character and atleast 1 special character";

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
             
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) {

                return new ValidationResult("Password is required");
            }

            string password=value.ToString();

            // for uppercase
            bool hasUpperCase=password.Any(char.IsUpper);

            // for special character
            bool hasSpecialChar= Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]");

            if (!hasUpperCase || !hasSpecialChar) {
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}
