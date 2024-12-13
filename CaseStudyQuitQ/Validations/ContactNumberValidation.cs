using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CaseStudyQuitQ.Validations {
    public class ContactNumberValidation : ValidationAttribute {

        public const string message = "Contact number must be 10 digits.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) {
                return new ValidationResult("Contact number is required.");
            }

            string contactNumber = value.ToString();

            if (!Regex.IsMatch(contactNumber, @"^\d{10}$")) {
                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
