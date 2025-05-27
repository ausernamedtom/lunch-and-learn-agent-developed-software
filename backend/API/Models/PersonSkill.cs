using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// Join entity representing a person's skill with associated proficiency level
    /// </summary>
    public class PersonSkill
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Foreign keys
        public string PersonId { get; set; } = string.Empty;
        public string SkillId { get; set; } = string.Empty;

        // Proficiency level as per ADR-002
        [Required]
        [EnumDataType(typeof(ProficiencyLevel))]
        public ProficiencyLevel ProficiencyLevel { get; set; }

        // Optional years of experience
        public int? YearsOfExperience { get; set; }

        // Indicates if the skill has been verified according to ADR-003
        [Required]
        public bool IsVerified { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PersonId))]
        public virtual Person? Person { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill? Skill { get; set; }
    }
}
