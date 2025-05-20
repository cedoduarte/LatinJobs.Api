using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class CreateUserAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [StringLength(256, ErrorMessage = "Email cannot be longer than 256 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password must be at leat 8 characters long")]
        public string? Password { get; set; }
    }
}
