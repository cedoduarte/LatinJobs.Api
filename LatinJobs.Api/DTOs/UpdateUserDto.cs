using LatinJobs.Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    [PasswordsMatch]
    public class UpdateUserDto : CreateUserDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
