using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Interface for people repository
    /// </summary>
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(string id);
        Task<Person> CreateAsync(Person person);
        Task<Person?> UpdateAsync(Person person);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<Person>> GetBySkillIdAsync(string skillId);
        Task<IEnumerable<Person>> GetBySkillIdAndMinimumProficiencyAsync(string skillId, ProficiencyLevel minProficiency);
        Task<IEnumerable<Person>> SearchAsync(string searchTerm);
    }

    /// <summary>
    /// Interface for skills repository
    /// </summary>
    public interface ISkillsRepository
    {
        Task<IEnumerable<Skill>> GetAllAsync();
        Task<Skill?> GetByIdAsync(string id);
        Task<Skill> CreateAsync(Skill skill);
        Task<Skill?> UpdateAsync(Skill skill);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<Skill>> GetByPersonIdAsync(string personId);
        Task<IEnumerable<Skill>> SearchAsync(string searchTerm);
        Task<IEnumerable<Skill>> GetByCategory(string category);
    }

    /// <summary>
    /// Interface for person skills repository
    /// </summary>
    public interface IPersonSkillsRepository
    {
        Task<PersonSkill?> GetAsync(string personId, string skillId);
        Task<PersonSkill?> GetByIdAsync(string id);
        Task<IEnumerable<PersonSkill>> GetByPersonIdAsync(string personId);
        Task<PersonSkill> CreateAsync(PersonSkill personSkill);
        Task<PersonSkill?> UpdateAsync(PersonSkill personSkill);
        Task<bool> DeleteAsync(string id);
        Task<bool> VerifyPersonSkillAsync(string id);
    }

    /// <summary>
    /// Interface for skill verifications repository
    /// </summary>
    public interface ISkillVerificationsRepository
    {
        Task<IEnumerable<SkillVerification>> GetByPersonSkillIdAsync(string personSkillId);
        Task<SkillVerification> CreateAsync(SkillVerification verification);
        Task<bool> DeleteAsync(string id);
    }
}
