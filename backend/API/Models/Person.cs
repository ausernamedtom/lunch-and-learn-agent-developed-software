using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Represents a person in the system
    /// </summary>
    public class Person
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? PhotoUrl { get; set; }

        // Navigation property for PersonSkills
        public virtual ICollection<PersonSkill> Skills { get; set; } = new List<PersonSkill>();

        // Computed property for full name
        public string FullName => $"{FirstName} {LastName}";
    }
}
