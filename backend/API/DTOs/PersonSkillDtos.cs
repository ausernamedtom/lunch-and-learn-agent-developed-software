using API.Models;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    /// <summary>
    /// DTO for a person's skill with proficiency level
    /// </summary>
    public class PersonSkillDto
    {
        public string Id { get; set; } = string.Empty;
        public string SkillId { get; set; } = string.Empty;
        public string SkillName { get; set; } = string.Empty;
        public string SkillDescription { get; set; } = string.Empty;
        public string SkillCategory { get; set; } = string.Empty;
        public ProficiencyLevel ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
        public bool IsVerified { get; set; }
        public List<VerificationDto> Verifications { get; set; } = new List<VerificationDto>();
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO for adding or updating a person's skill
    /// </summary>
    public class PersonSkillCreateUpdateDto
    {
        public string SkillId { get; set; } = string.Empty;
        public ProficiencyLevel ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
    }

    /// <summary>
    /// DTO for verification details
    /// </summary>
    public class VerificationDto
    {
        public string Id { get; set; } = string.Empty;
        public string VerificationType { get; set; } = string.Empty;
        public string? VerifiedBy { get; set; }
        public string? Note { get; set; }
        public string? CertificationUrl { get; set; }
        public DateTime VerificationDate { get; set; }
    }

    /// <summary>
    /// DTO for creating a skill verification
    /// </summary>
    public class VerificationCreateDto
    {
        public string PersonSkillId { get; set; } = string.Empty;
        public string VerificationType { get; set; } = string.Empty;
        public string? VerifiedBy { get; set; }
        public string? Note { get; set; }
        public string? CertificationUrl { get; set; }
    }
}
