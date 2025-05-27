using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Represents a skill that a person can have
    /// </summary>
    public class Skill
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        // Navigation property for PersonSkills
        public virtual ICollection<PersonSkill> PersonSkills { get; set; } = new List<PersonSkill>();
    }
}
