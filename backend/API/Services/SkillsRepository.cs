using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Implementation of the skills repository
    /// </summary>
    public class SkillsRepository : ISkillsRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills
                .Include(s => s.PersonSkills)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Skill?> GetByIdAsync(string id)
        {
            return await _context.Skills
                .Include(s => s.PersonSkills)
                    .ThenInclude(ps => ps.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill> CreateAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<Skill?> UpdateAsync(Skill skill)
        {
            var existingSkill = await _context.Skills.FindAsync(skill.Id);
            if (existingSkill == null)
            {
                return null;
            }

            _context.Entry(existingSkill).State = EntityState.Detached;
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return false;
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Skill>> GetByPersonIdAsync(string personId)
        {
            return await _context.PersonSkills
                .Include(ps => ps.Skill)
                .Where(ps => ps.PersonId == personId)
                .Select(ps => ps.Skill!)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Skill>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            searchTerm = searchTerm.ToLower();
            
            return await _context.Skills
                .Where(s => 
                    s.Name.ToLower().Contains(searchTerm) ||
                    s.Description.ToLower().Contains(searchTerm) ||
                    s.Category.ToLower().Contains(searchTerm))
                .Include(s => s.PersonSkills)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Skill>> GetByCategory(string category)
        {
            return await _context.Skills
                .Where(s => s.Category.ToLower() == category.ToLower())
                .Include(s => s.PersonSkills)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
