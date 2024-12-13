using System.ComponentModel.DataAnnotations;

namespace CaseStudyQuitQ.Validations {
    public class RoleValidation:ValidationAttribute { //ValidationAttribute: It helps in creating custom validation by overriding its IsValid method.

        public const string message = "Role should be either: customer or seller or admin";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

            var valueString = value != null ? value.ToString() : null;
            if (string.IsNullOrWhiteSpace(valueString)) {

                return new ValidationResult("Role is required");
            }

            if(valueString!="customer" && valueString!="seller" && valueString != "admin") {
                return new ValidationResult(message);
            }
            
            return ValidationResult.Success;

        }
    }
}
