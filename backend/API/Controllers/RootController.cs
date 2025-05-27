using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// Root API endpoint that provides links to main resources (HATEOAS)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, string>), 200)]
        public IActionResult GetRoot()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/api";
            
            // Create a dictionary of links to all available resources
            var links = new Dictionary<string, string>
            {
                { "self", baseUrl },
                { "people", $"{baseUrl}/people" },
                { "skills", $"{baseUrl}/skills" },
                { "documentation", $"{Request.Scheme}://{Request.Host}{Request.PathBase}/swagger" }
            };
            
            return Ok(links);
        }
    }
}
