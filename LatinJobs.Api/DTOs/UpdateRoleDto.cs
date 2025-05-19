using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class UpdateRoleDto : CreateRoleDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
