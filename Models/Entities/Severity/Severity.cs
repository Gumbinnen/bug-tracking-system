﻿using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class Severity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }
    }
}
