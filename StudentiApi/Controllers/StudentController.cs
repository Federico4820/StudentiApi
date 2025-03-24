using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentiApi.Models;
using StudentiApi.Services;

namespace StudentiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
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

            return Ok(new
            {
                message = text,
                products = studentList
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            var result = await _studentService.AddStudentAsync(student);

            if (!result)
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
    }
}
