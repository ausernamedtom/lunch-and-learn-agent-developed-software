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
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPersonSkillsRepository _personSkillsRepository;
        private readonly MappingService _mappingService;

        public PeopleController(
            IPeopleRepository peopleRepository,
            IPersonSkillsRepository personSkillsRepository,
            MappingService mappingService)
        {
            _peopleRepository = peopleRepository;
            _personSkillsRepository = personSkillsRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Get all people with a summary of their top skills
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonSummaryDto>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            IEnumerable<Person> people;
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                people = await _peopleRepository.SearchAsync(search);
            }
            else
            {
                people = await _peopleRepository.GetAllAsync();
            }
            
            var personDtos = people.Select(p => _mappingService.ToPersonSummaryDto(p));
            return Ok(personDtos);
        }

        /// <summary>
        /// Get a specific person by ID with all their skills
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var person = await _peopleRepository.GetByIdAsync(id);
            
            if (person == null)
            {
                return NotFound();
            }
            
            var personDto = _mappingService.ToPersonDetailDto(person);
            return Ok(personDto);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PersonDetailDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(PersonCreateUpdateDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var person = _mappingService.ToPersonEntity(personDto);
            await _peopleRepository.CreateAsync(person);
            
            var createdPersonDto = _mappingService.ToPersonDetailDto(person);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, createdPersonDto);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PersonDetailDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(string id, PersonCreateUpdateDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var existingPerson = await _peopleRepository.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound();
            }
            
            _mappingService.UpdatePersonEntity(existingPerson, personDto);
            await _peopleRepository.UpdateAsync(existingPerson);
            
            var updatedPersonDto = _mappingService.ToPersonDetailDto(existingPerson);
            return Ok(updatedPersonDto);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _peopleRepository.DeleteAsync(id);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        /// <summary>
        /// Get all skills for a specific person
        /// </summary>
        [HttpGet("{id}/skills")]
        [ProducesResponseType(typeof(IEnumerable<PersonSkillDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSkills(string id)
        {
            var person = await _peopleRepository.GetByIdAsync(id);
            
            if (person == null)
            {
                return NotFound();
            }
            
            var personDto = _mappingService.ToPersonDetailDto(person);
            return Ok(personDto.Skills);
        }

        /// <summary>
        /// Add a skill to a person
        /// </summary>
        [HttpPost("{id}/skills")]
        [ProducesResponseType(typeof(PersonSkillDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddSkill(string id, PersonSkillCreateUpdateDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var person = await _peopleRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            
            // Check if the person already has this skill
            var existingSkill = await _personSkillsRepository.GetAsync(id, skillDto.SkillId);
            if (existingSkill != null)
            {
                return BadRequest("Person already has this skill.");
            }
            
            var personSkill = _mappingService.ToPersonSkillEntity(skillDto, id);
            await _personSkillsRepository.CreateAsync(personSkill);
            
            // Refresh to get the complete person with skills
            person = await _peopleRepository.GetByIdAsync(id);
            var personDto = _mappingService.ToPersonDetailDto(person!);
            var addedSkill = personDto.Skills.FirstOrDefault(s => s.SkillId == skillDto.SkillId);
            
            return CreatedAtAction(nameof(GetSkills), new { id = person!.Id }, addedSkill);
        }

        /// <summary>
        /// Update a person's skill
        /// </summary>
        [HttpPut("{personId}/skills/{skillId}")]
        [ProducesResponseType(typeof(PersonSkillDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateSkill(string personId, string skillId, PersonSkillCreateUpdateDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var personSkill = await _personSkillsRepository.GetAsync(personId, skillId);
            if (personSkill == null)
            {
                return NotFound();
            }
            
            _mappingService.UpdatePersonSkillEntity(personSkill, skillDto);
            await _personSkillsRepository.UpdateAsync(personSkill);
            
            // Refresh to get the updated person with skills
            var person = await _peopleRepository.GetByIdAsync(personId);
            var personDto = _mappingService.ToPersonDetailDto(person!);
            var updatedSkill = personDto.Skills.FirstOrDefault(s => s.SkillId == skillId);
            
            return Ok(updatedSkill);
        }

        /// <summary>
        /// Remove a skill from a person
        /// </summary>
        [HttpDelete("{personId}/skills/{skillId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveSkill(string personId, string skillId)
        {
            var personSkill = await _personSkillsRepository.GetAsync(personId, skillId);
            if (personSkill == null)
            {
                return NotFound();
            }
            
            var result = await _personSkillsRepository.DeleteAsync(personSkill.Id);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
