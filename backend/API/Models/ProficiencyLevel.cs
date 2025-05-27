using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Represents skill proficiency levels as per ADR-002
    /// </summary>
    public enum ProficiencyLevel
    {
        /// <summary>
        /// Basic theoretical knowledge, limited practical experience
        /// </summary>
        Novice = 1,
        
        /// <summary>
        /// Can apply the skill with guidance and supervision
        /// </summary>
        Beginner = 2,
        
        /// <summary>
        /// Can work independently on most tasks related to this skill
        /// </summary>
        Intermediate = 3,
        
        /// <summary>
        /// Deep knowledge, can solve complex problems, can teach others
        /// </summary>
        Advanced = 4,
        
        /// <summary>
        /// Authoritative knowledge, industry recognition, can innovate in this area
        /// </summary>
        Expert = 5
    }
}
