using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Implementation of the people repository
    /// </summary>
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ApplicationDbContext _context;

        public PeopleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People
                .Include(p => p.Skills)
                    .ThenInclude(ps => ps.Skill)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(string id)
        {
            return await _context.People
                .Include(p => p.Skills)
                    .ThenInclude(ps => ps.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person?> UpdateAsync(Person person)
        {
            var existingPerson = await _context.People.FindAsync(person.Id);
            if (existingPerson == null)
            {
                return null;
            }

            _context.Entry(existingPerson).State = EntityState.Detached;
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return false;
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Person>> GetBySkillIdAsync(string skillId)
        {
            return await _context.People
                .Include(p => p.Skills)
                    .ThenInclude(ps => ps.Skill)
                .Where(p => p.Skills.Any(s => s.SkillId == skillId))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetBySkillIdAndMinimumProficiencyAsync(string skillId, ProficiencyLevel minProficiency)
        {
            return await _context.People
                .Include(p => p.Skills)
                    .ThenInclude(ps => ps.Skill)
                .Where(p => p.Skills.Any(s => s.SkillId == skillId && (int)s.ProficiencyLevel >= (int)minProficiency))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            searchTerm = searchTerm.ToLower();
            
            return await _context.People
                .Include(p => p.Skills)
                    .ThenInclude(ps => ps.Skill)
                .Where(p => 
                    p.FirstName.ToLower().Contains(searchTerm) ||
                    p.LastName.ToLower().Contains(searchTerm) ||
                    p.Email.ToLower().Contains(searchTerm) ||
                    p.Department.ToLower().Contains(searchTerm) ||
                    p.JobTitle.ToLower().Contains(searchTerm) ||
                    p.Skills.Any(ps => ps.Skill != null && ps.Skill.Name.ToLower().Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
