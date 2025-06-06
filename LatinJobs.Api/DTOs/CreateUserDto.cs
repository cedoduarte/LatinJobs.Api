﻿using LatinJobs.Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{

    [PasswordsMatch]
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password must be at leat 8 characters long")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password confirmation must be at leat 8 characters long")]
        public string? PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "First name must be at least 1 characters long and cannot be longer than 256 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "Last name must be at least 1 character long and cannot be longer than 256 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [StringLength(256, ErrorMessage = "Email cannot be longer than 256 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Role ID is required")]
        public int? RoleId { get; set; }
    }
}
