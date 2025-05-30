﻿using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class UserAuthentication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public virtual User? User { get; set; }
    }
}
