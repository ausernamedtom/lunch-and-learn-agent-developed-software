using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// Represents a verification record for a person's skill as per ADR-003
    /// </summary>
    public class SkillVerification
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        // Foreign key to PersonSkill
        public string PersonSkillId { get; set; } = string.Empty;
        
        // Type of verification performed
        [Required]
        [MaxLength(50)]
        public string VerificationType { get; set; } = string.Empty; // e.g., "PeerEndorsement", "ManagerVerification", "CertificationUpload", "SkillChallenge"

        // The user who performed the verification (if applicable)
        [MaxLength(100)]
        public string? VerifiedBy { get; set; }
        
        // Optional comment/note about the verification
        [MaxLength(500)]
        public string? Note { get; set; }
        
        // Optional URL to certification document
        [MaxLength(255)]
        public string? CertificationUrl { get; set; }
        
        // Timestamp for when the verification occurred
        [Required]
        public DateTime VerificationDate { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        [ForeignKey(nameof(PersonSkillId))]
        public virtual PersonSkill? PersonSkill { get; set; }
    }
}
