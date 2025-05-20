using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class CreateUserRoleDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Role ID is required")]
        public int? RoleId { get; set; }
    }
}
