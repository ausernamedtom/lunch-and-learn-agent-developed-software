using API.Models;
using System.Collections.Generic;

namespace API.DTOs
{
    /// <summary>
    /// DTO for Skill with essential information for list views
    /// </summary>
    public class SkillSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        
        // For displaying in person cards (optional)
        public ProficiencyLevel? ProficiencyLevel { get; set; }
        public bool? IsVerified { get; set; }
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO for detailed Skill information
    /// </summary>
    public class SkillDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        
        // People with this skill
        public List<PersonWithProficiencyDto> People { get; set; } = new List<PersonWithProficiencyDto>();
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO for creating or updating a Skill
    /// </summary>
    public class SkillCreateUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// DTO for showing a person with their proficiency for a specific skill
    /// </summary>
    public class PersonWithProficiencyDto
    {
        public string PersonId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public ProficiencyLevel ProficiencyLevel { get; set; }
        public bool IsVerified { get; set; }
        public int? YearsOfExperience { get; set; }
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }
}
