using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            if (compensation == null)
            {
                return BadRequest(); //Invalid compensation
            }

            var createdCompensation = _compensationService.CreateCompensation(compensation);

            return CreatedAtRoute("GetCompensationByEmployeeId", new { employeeId = createdCompensation.Employee }, createdCompensation);
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetCompensationByEmployeeId(string employeeId)
        {
            _logger.LogDebug($"Received get compensation by employeeId request for '{employeeId}'");

            var compensation = _compensationService.ReadCompensationByEmployeeId(employeeId);

            if (compensation == null)
            {
                return NotFound(); // Compensation not found
            }

            return Ok(compensation);
        }
    }
}
