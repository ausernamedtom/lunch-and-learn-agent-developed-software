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
    /// Implementation of the person skills repository
    /// </summary>
    public class PersonSkillsRepository : IPersonSkillsRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonSkillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonSkill?> GetAsync(string personId, string skillId)
        {
            return await _context.PersonSkills
                .Include(ps => ps.Person)
                .Include(ps => ps.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(ps => ps.PersonId == personId && ps.SkillId == skillId);
        }

        public async Task<PersonSkill?> GetByIdAsync(string id)
        {
            return await _context.PersonSkills
                .Include(ps => ps.Person)
                .Include(ps => ps.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(ps => ps.Id == id);
        }

        public async Task<IEnumerable<PersonSkill>> GetByPersonIdAsync(string personId)
        {
            return await _context.PersonSkills
                .Include(ps => ps.Skill)
                .Where(ps => ps.PersonId == personId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PersonSkill> CreateAsync(PersonSkill personSkill)
        {
            _context.PersonSkills.Add(personSkill);
            await _context.SaveChangesAsync();
            return personSkill;
        }

        public async Task<PersonSkill?> UpdateAsync(PersonSkill personSkill)
        {
            var existingPersonSkill = await _context.PersonSkills.FindAsync(personSkill.Id);
            if (existingPersonSkill == null)
            {
                return null;
            }

            _context.Entry(existingPersonSkill).State = EntityState.Detached;
            _context.PersonSkills.Update(personSkill);
            await _context.SaveChangesAsync();
            return personSkill;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var personSkill = await _context.PersonSkills.FindAsync(id);
            if (personSkill == null)
            {
                return false;
            }

            _context.PersonSkills.Remove(personSkill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyPersonSkillAsync(string id)
        {
            var personSkill = await _context.PersonSkills.FindAsync(id);
            if (personSkill == null)
            {
                return false;
            }

            personSkill.IsVerified = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }

    /// <summary>
    /// Implementation of the skill verifications repository
    /// </summary>
    public class SkillVerificationsRepository : ISkillVerificationsRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillVerificationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SkillVerification>> GetByPersonSkillIdAsync(string personSkillId)
        {
            return await _context.SkillVerifications
                .Where(sv => sv.PersonSkillId == personSkillId)
                .Include(sv => sv.PersonSkill)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SkillVerification> CreateAsync(SkillVerification verification)
        {
            _context.SkillVerifications.Add(verification);
            await _context.SaveChangesAsync();
            
            // Automatically mark the PersonSkill as verified when a verification is added
            var personSkill = await _context.PersonSkills.FindAsync(verification.PersonSkillId);
            if (personSkill != null && !personSkill.IsVerified)
            {
                personSkill.IsVerified = true;
                await _context.SaveChangesAsync();
            }
            
            return verification;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var verification = await _context.SkillVerifications.FindAsync(id);
            if (verification == null)
            {
                return false;
            }

            _context.SkillVerifications.Remove(verification);
            await _context.SaveChangesAsync();
            
            // Check if this was the last verification for the skill, and if so, update IsVerified flag
            var remainingVerifications = await _context.SkillVerifications
                .Where(sv => sv.PersonSkillId == verification.PersonSkillId)
                .AnyAsync();
                
            if (!remainingVerifications)
            {
                var personSkill = await _context.PersonSkills.FindAsync(verification.PersonSkillId);
                if (personSkill != null)
                {
                    personSkill.IsVerified = false;
                    await _context.SaveChangesAsync();
                }
            }
            
            return true;
        }
    }
}
