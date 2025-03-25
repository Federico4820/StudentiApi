using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentiApi.DTOs;
using StudentiApi.Models;
using StudentiApi.Services;

namespace StudentiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(StudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studentList = await _studentService.GetStudentsAsync();

            if (studentList == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto!"
                });
            }

            if (!studentList.Any())
            {
                return NoContent();
            }

            var count = studentList.Count();

            var text = count == 1 ? $"{count} studente trovato" : $"{count} studenti trovati";

            _logger.LogInformation($"Requesting customers info: {JsonSerializer.Serialize(studentList, new JsonSerializerOptions() { WriteIndented = true })}");

            return Ok(new
            {
                message = text,
                products = studentList
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddStudentRequestDto student)
        {
            var result = await _studentService.AddStudentAsync(student);

            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto!"
                });
            }

            return Ok(new
            {
                message = "Studente aggiunto con successo!"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _studentService.GetStudentByIdAsync(id);

            return result != null ? Ok(new { message = "Customer found", customer = result }) : BadRequest(new { message = "Something went wrong" });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update (Guid id, [FromBody] StudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                Email = studentDto.Email
            };
            var result = await _studentService.UpdateStudentAsync(id, student);

            return result ? Ok(new { message = "Customer updated" }) : BadRequest(new { message = "Something went wrong" });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            return result ? Ok(new { message = "Customer deleted" }) : BadRequest(new { message = "Something went wrong" });
        }
    }
}
