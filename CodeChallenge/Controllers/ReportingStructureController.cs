using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetReportingStructure(string employeeId)
        {
            _logger.LogDebug($"Received get reporting structure request for '{employeeId}'");

            // Find the employee with the specified employeeId
            var employee = _employeeService.GetById(employeeId);

            if (employee == null)
            {
                return NotFound(); // Employee not found
            }

            // Calculate the number of reports
            var numberOfReports = CalculateNumberOfReports(employee);

            // Create the ReportingStructure object
            var reportingStructure = new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfReports
            };

            return Ok(reportingStructure);
        }

        private int CalculateNumberOfReports(Employee employee)
        {
            // Recursively count the number of reports for the given employee and all their direct reports
            var numberOfReports = employee.DirectReports.Count;

            foreach (var dirRep in employee.DirectReports)
            {
                var directReport = _employeeService.GetById(dirRep.EmployeeId);
                if (directReport != null)
                {
                    numberOfReports += CalculateNumberOfReports(directReport);
                }
            }

            return numberOfReports;
        }
    }
}