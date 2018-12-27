using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EAP_P.Models;
using Microsoft.AspNetCore.Cors;

namespace EAP_P.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EAP_PContext _context;

        public EmployeesController(EAP_PContext context)
        {
            _context = context;
            var count = context.Employee.Count(t => t.StudentID > 0);
            if (count <= 0)
            {
                _context.Employee.Add(new Employee
                {
                    FirstName = "Nhan",
                    LastName = "Le Dang",
                    PhoneNumber = "123456",
                    Email = "nhan@gmail.com"
                });
                _context.Employee.Add(new Employee
                {
                    FirstName = "Nhung",
                    LastName = "Doan Thi",
                    PhoneNumber = "123457",
                    Email = "nhung@gmail.com"
                });
                _context.Employee.Add(new Employee
                {
                    FirstName = "Tuan",
                    LastName = "Luu Cung",
                    PhoneNumber = "123458",
                    Email = "tuan@gmail.com"
                });
                _context.Employee.Add(new Employee
                {
                    FirstName = "Hai",
                    LastName = "Nguyen Van",
                    PhoneNumber = "123459",
                    Email = "Hai@gmail.com"
                });
                _context.Employee.Add(new Employee
                {
                    FirstName = "Vien",
                    LastName = "Le Van",
                    PhoneNumber = "123450",
                    Email = "vien@gmail.com"
                });
                _context.SaveChanges();
            }
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] long id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.StudentID }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employee.Any(e => e.StudentID == id);
        }
    }
}