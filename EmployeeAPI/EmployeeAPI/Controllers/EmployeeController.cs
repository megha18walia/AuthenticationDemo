using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ILogger<EmployeeController> logger;
        public EmployeeController(ILogger<EmployeeController> _logger)
        {
            logger = _logger;
        }

        [Route("Employees")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employee> empList = new List<Employee>();
            empList.Add(new Employee { EmployeeID = 1, Name = "ABC0", Age = 12, Designation = "Engineer0" });
            empList.Add(new Employee { EmployeeID = 2, Name = "ABC1", Age = 14, Designation = "Engineer1" });
            empList.Add(new Employee { EmployeeID = 3, Name = "ABC2", Age = 16, Designation = "Engineer2" });
            logger.LogInformation("Employee get method executed ", empList);
            return Ok(empList);
        }

        [Route("Employees/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            Employee emp = new Employee { EmployeeID = id, Name = "ABC0", Age = 12, Designation = "Engineer0" };
            return Ok(emp);
        }
    }
}