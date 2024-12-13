using CaseStudyQuitQ.Validations;
using System.ComponentModel.DataAnnotations;

namespace CaseStudyQuitQ.Authentication {
    public class RegisterModel {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [ContactNumberValidation]
        [Required(ErrorMessage = "Contact details is required")]
        public string ContactNumber { get; set; }

        [PasswordValidation]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [RoleValidation]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
