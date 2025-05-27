using API.Models;
using System.Collections.Generic;

namespace API.DTOs
{
    /// <summary>
    /// DTO for Person with essential information for list views
    /// </summary>
    public class PersonSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        
        // For displaying top skills in summary views
        public List<SkillSummaryDto> TopSkills { get; set; } = new List<SkillSummaryDto>();
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO for detailed Person information including all skills
    /// </summary>
    public class PersonDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public List<PersonSkillDto> Skills { get; set; } = new List<PersonSkillDto>();
        
        // Navigation links (HATEOAS)
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// DTO for creating or updating a Person
    /// </summary>
    public class PersonCreateUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
    }
}
