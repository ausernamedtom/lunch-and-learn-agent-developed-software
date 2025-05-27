using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    /// <summary>
    /// Service for mapping between entities and DTOs, and adding HATEOAS links
    /// </summary>
    public class MappingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MappingService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return string.Empty;
            
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return baseUrl;
        }

        // Person mapping methods
        public PersonSummaryDto ToPersonSummaryDto(Person person)
        {
            var baseUrl = GetBaseUrl();
            var dto = new PersonSummaryDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                JobTitle = person.JobTitle,
                Department = person.Department,
                PhotoUrl = person.PhotoUrl,
                Links = new Dictionary<string, string>
                {
                    { "self", $"{baseUrl}/api/people/{person.Id}" },
                    { "skills", $"{baseUrl}/api/people/{person.Id}/skills" }
                }
            };

            // Add top 3 skills by proficiency level
            if (person.Skills != null)
            {
                var topSkills = person.Skills
                    .OrderByDescending(s => (int)s.ProficiencyLevel)
                    .Take(3)
                    .ToList();
                
                foreach (var personSkill in topSkills)
                {
                    if (personSkill.Skill != null)
                    {
                        dto.TopSkills.Add(new SkillSummaryDto
                        {
                            Id = personSkill.Skill.Id,
                            Name = personSkill.Skill.Name,
                            Category = personSkill.Skill.Category,
                            ProficiencyLevel = personSkill.ProficiencyLevel,
                            IsVerified = personSkill.IsVerified,
                            Links = new Dictionary<string, string>
                            {
                                { "self", $"{baseUrl}/api/skills/{personSkill.Skill.Id}" }
                            }
                        });
                    }
                }
            }

            return dto;
        }

        public PersonDetailDto ToPersonDetailDto(Person person)
        {
            var baseUrl = GetBaseUrl();
            var dto = new PersonDetailDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                JobTitle = person.JobTitle,
                Department = person.Department,
                Email = person.Email,
                PhotoUrl = person.PhotoUrl,
                Links = new Dictionary<string, string>
                {
                    { "self", $"{baseUrl}/api/people/{person.Id}" },
                    { "skills", $"{baseUrl}/api/people/{person.Id}/skills" }
                }
            };

            // Add all skills
            if (person.Skills != null)
            {
                foreach (var personSkill in person.Skills)
                {
                    if (personSkill.Skill != null)
                    {
                        dto.Skills.Add(new PersonSkillDto
                        {
                            Id = personSkill.Id,
                            SkillId = personSkill.Skill.Id,
                            SkillName = personSkill.Skill.Name,
                            SkillDescription = personSkill.Skill.Description,
                            SkillCategory = personSkill.Skill.Category,
                            ProficiencyLevel = personSkill.ProficiencyLevel,
                            YearsOfExperience = personSkill.YearsOfExperience,
                            IsVerified = personSkill.IsVerified,
                            Links = new Dictionary<string, string>
                            {
                                { "skill", $"{baseUrl}/api/skills/{personSkill.Skill.Id}" },
                                { "verifications", $"{baseUrl}/api/personskills/{personSkill.Id}/verifications" }
                            }
                        });
                    }
                }
            }

            return dto;
        }

        public Person ToPersonEntity(PersonCreateUpdateDto dto)
        {
            return new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobTitle = dto.JobTitle,
                Department = dto.Department,
                Email = dto.Email,
                PhotoUrl = dto.PhotoUrl
            };
        }

        public void UpdatePersonEntity(Person person, PersonCreateUpdateDto dto)
        {
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.JobTitle = dto.JobTitle;
            person.Department = dto.Department;
            person.Email = dto.Email;
            person.PhotoUrl = dto.PhotoUrl;
        }

        // Skill mapping methods
        public SkillSummaryDto ToSkillSummaryDto(Skill skill)
        {
            var baseUrl = GetBaseUrl();
            return new SkillSummaryDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Category = skill.Category,
                Links = new Dictionary<string, string>
                {
                    { "self", $"{baseUrl}/api/skills/{skill.Id}" },
                    { "people", $"{baseUrl}/api/skills/{skill.Id}/people" }
                }
            };
        }

        public SkillDetailDto ToSkillDetailDto(Skill skill)
        {
            var baseUrl = GetBaseUrl();
            var dto = new SkillDetailDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                Category = skill.Category,
                Links = new Dictionary<string, string>
                {
                    { "self", $"{baseUrl}/api/skills/{skill.Id}" },
                    { "people", $"{baseUrl}/api/skills/{skill.Id}/people" }
                }
            };

            // Add people with this skill
            if (skill.PersonSkills != null)
            {
                foreach (var personSkill in skill.PersonSkills)
                {
                    if (personSkill.Person != null)
                    {
                        dto.People.Add(new PersonWithProficiencyDto
                        {
                            PersonId = personSkill.Person.Id,
                            FirstName = personSkill.Person.FirstName,
                            LastName = personSkill.Person.LastName,
                            JobTitle = personSkill.Person.JobTitle,
                            PhotoUrl = personSkill.Person.PhotoUrl,
                            ProficiencyLevel = personSkill.ProficiencyLevel,
                            IsVerified = personSkill.IsVerified,
                            YearsOfExperience = personSkill.YearsOfExperience,
                            Links = new Dictionary<string, string>
                            {
                                { "self", $"{baseUrl}/api/people/{personSkill.Person.Id}" }
                            }
                        });
                    }
                }
            }

            return dto;
        }

        public Skill ToSkillEntity(SkillCreateUpdateDto dto)
        {
            return new Skill
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category
            };
        }

        public void UpdateSkillEntity(Skill skill, SkillCreateUpdateDto dto)
        {
            skill.Name = dto.Name;
            skill.Description = dto.Description;
            skill.Category = dto.Category;
        }

        // PersonSkill mapping methods
        public PersonSkill ToPersonSkillEntity(PersonSkillCreateUpdateDto dto, string personId)
        {
            return new PersonSkill
            {
                PersonId = personId,
                SkillId = dto.SkillId,
                ProficiencyLevel = dto.ProficiencyLevel,
                YearsOfExperience = dto.YearsOfExperience,
                IsVerified = false // New skills start unverified
            };
        }

        public void UpdatePersonSkillEntity(PersonSkill personSkill, PersonSkillCreateUpdateDto dto)
        {
            personSkill.ProficiencyLevel = dto.ProficiencyLevel;
            personSkill.YearsOfExperience = dto.YearsOfExperience;
            // Note: We don't update SkillId or PersonId as those are identity fields
            // Note: We don't update IsVerified as that should only be updated through verification process
        }

        // Verification mapping methods
        public VerificationDto ToVerificationDto(SkillVerification verification)
        {
            var baseUrl = GetBaseUrl();
            return new VerificationDto
            {
                Id = verification.Id,
                VerificationType = verification.VerificationType,
                VerifiedBy = verification.VerifiedBy,
                Note = verification.Note,
                CertificationUrl = verification.CertificationUrl,
                VerificationDate = verification.VerificationDate
            };
        }

        public SkillVerification ToVerificationEntity(VerificationCreateDto dto)
        {
            return new SkillVerification
            {
                PersonSkillId = dto.PersonSkillId,
                VerificationType = dto.VerificationType,
                VerifiedBy = dto.VerifiedBy,
                Note = dto.Note,
                CertificationUrl = dto.CertificationUrl,
                VerificationDate = DateTime.UtcNow
            };
        }
    }
}
