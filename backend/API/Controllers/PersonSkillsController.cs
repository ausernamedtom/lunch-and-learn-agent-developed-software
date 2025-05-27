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
    public class PersonSkillsController : ControllerBase
    {
        private readonly IPersonSkillsRepository _personSkillsRepository;
        private readonly ISkillVerificationsRepository _verificationsRepository;
        private readonly MappingService _mappingService;

        public PersonSkillsController(
            IPersonSkillsRepository personSkillsRepository,
            ISkillVerificationsRepository verificationsRepository,
            MappingService mappingService)
        {
            _personSkillsRepository = personSkillsRepository;
            _verificationsRepository = verificationsRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Get verification records for a person's skill
        /// </summary>
        [HttpGet("{personSkillId}/verifications")]
        [ProducesResponseType(typeof(IEnumerable<VerificationDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVerifications(string personSkillId)
        {
            var personSkill = await _personSkillsRepository.GetByIdAsync(personSkillId);
            
            if (personSkill == null)
            {
                return NotFound();
            }
            
            var verifications = await _verificationsRepository.GetByPersonSkillIdAsync(personSkillId);
            var verificationDtos = verifications.Select(v => _mappingService.ToVerificationDto(v));
            
            return Ok(verificationDtos);
        }

        /// <summary>
        /// Create a verification record for a person's skill
        /// </summary>
        [HttpPost("{personSkillId}/verifications")]
        [ProducesResponseType(typeof(VerificationDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddVerification(string personSkillId, VerificationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var personSkill = await _personSkillsRepository.GetByIdAsync(personSkillId);
            
            if (personSkill == null)
            {
                return NotFound();
            }
            
            // Override any personSkillId in the DTO with the one from the URL
            dto.PersonSkillId = personSkillId;
            
            var verification = _mappingService.ToVerificationEntity(dto);
            await _verificationsRepository.CreateAsync(verification);
            
            var verificationDto = _mappingService.ToVerificationDto(verification);
            return CreatedAtAction(nameof(GetVerifications), new { personSkillId }, verificationDto);
        }

        /// <summary>
        /// Delete a verification record
        /// </summary>
        [HttpDelete("{personSkillId}/verifications/{verificationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVerification(string personSkillId, string verificationId)
        {
            var personSkill = await _personSkillsRepository.GetByIdAsync(personSkillId);
            
            if (personSkill == null)
            {
                return NotFound();
            }
            
            var result = await _verificationsRepository.DeleteAsync(verificationId);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
