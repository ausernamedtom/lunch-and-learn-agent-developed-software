using API.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly MappingService _mappingService;

        public SkillsController(
            ISkillsRepository skillsRepository,
            IPeopleRepository peopleRepository,
            MappingService mappingService)
        {
            _skillsRepository = skillsRepository;
            _peopleRepository = peopleRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Get all skills
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SkillSummaryDto>), 200)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search = null,
            [FromQuery] string? category = null)
        {
            IEnumerable<Skill> skills;
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                skills = await _skillsRepository.SearchAsync(search);
            }
            else if (!string.IsNullOrWhiteSpace(category))
            {
                skills = await _skillsRepository.GetByCategory(category);
            }
            else
            {
                skills = await _skillsRepository.GetAllAsync();
            }
            
            var skillDtos = skills.Select(s => _mappingService.ToSkillSummaryDto(s));
            return Ok(skillDtos);
        }

        /// <summary>
        /// Get a specific skill by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SkillDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var skill = await _skillsRepository.GetByIdAsync(id);
            
            if (skill == null)
            {
                return NotFound();
            }
            
            var skillDto = _mappingService.ToSkillDetailDto(skill);
            return Ok(skillDto);
        }

        /// <summary>
        /// Create a new skill
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SkillDetailDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(SkillCreateUpdateDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var skill = _mappingService.ToSkillEntity(skillDto);
            await _skillsRepository.CreateAsync(skill);
            
            var createdSkillDto = _mappingService.ToSkillDetailDto(skill);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, createdSkillDto);
        }

        /// <summary>
        /// Update an existing skill
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SkillDetailDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(string id, SkillCreateUpdateDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var existingSkill = await _skillsRepository.GetByIdAsync(id);
            if (existingSkill == null)
            {
                return NotFound();
            }
            
            _mappingService.UpdateSkillEntity(existingSkill, skillDto);
            await _skillsRepository.UpdateAsync(existingSkill);
            
            var updatedSkillDto = _mappingService.ToSkillDetailDto(existingSkill);
            return Ok(updatedSkillDto);
        }

        /// <summary>
        /// Delete a skill
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _skillsRepository.DeleteAsync(id);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        /// <summary>
        /// Get all people with a specific skill
        /// </summary>
        [HttpGet("{id}/people")]
        [ProducesResponseType(typeof(IEnumerable<PersonWithProficiencyDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPeople(
            string id,
            [FromQuery] ProficiencyLevel? minProficiency = null)
        {
            var skill = await _skillsRepository.GetByIdAsync(id);
            
            if (skill == null)
            {
                return NotFound();
            }
            
            IEnumerable<Person> people;
            
            if (minProficiency.HasValue)
            {
                people = await _peopleRepository.GetBySkillIdAndMinimumProficiencyAsync(id, minProficiency.Value);
            }
            else
            {
                people = await _peopleRepository.GetBySkillIdAsync(id);
            }
            
            var skillDto = _mappingService.ToSkillDetailDto(skill);
            
            // If we filtered by proficiency, we need to further filter the people in the DTO
            if (minProficiency.HasValue)
            {
                skillDto.People = skillDto.People
                    .Where(p => (int)p.ProficiencyLevel >= (int)minProficiency.Value)
                    .ToList();
            }
            
            return Ok(skillDto.People);
        }
    }
}
