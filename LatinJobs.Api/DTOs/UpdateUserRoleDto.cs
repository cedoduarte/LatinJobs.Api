using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class UpdateUserRoleDto : CreateUserRoleDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
