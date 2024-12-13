using System.ComponentModel.DataAnnotations;

namespace QuitQBackend.Authentication {
    public class RegisterModel {
        [Required(ErrorMessage = "User Name is required")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Contact details is required")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
