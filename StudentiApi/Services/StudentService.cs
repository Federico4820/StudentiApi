using StudentiApi.Data;
using Microsoft.EntityFrameworkCore;
using StudentiApi.Models;
using StudentiApi.DTOs;

namespace StudentiApi.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudentService> _logger;

        public StudentService(ApplicationDbContext context, ILogger<StudentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                _logger.LogInformation("a");
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex, "b");
                return false;
            }
        }

        public async Task<StudentDto?> AddStudentAsync(AddStudentRequestDto addStudentRequestDto)
        {
            try
            {
                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = addStudentRequestDto.Name,
                    Surname = addStudentRequestDto.Surname,
                    Email = addStudentRequestDto.Email,
                    Signed = DateTime.Now
                };

                _context.Students.Add(student);
                if (!await SaveAsync())
                {
                    return null;
                }

                return new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Email = student.Email,
                    Signed = student.Signed
                };

                //_context.Students.Add(student);
                //return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<StudentDto>?> GetStudentsAsync()
        {
            try
            {
                var students = await _context.Students.ToListAsync();
                return students.Select(student => new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Email = student.Email,
                    Signed = student.Signed
                    
                }).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var existingStudent = await _context.Students.FirstOrDefaultAsync(c => c.Id == id);

                var customerDto = new Student()
                {
                    Id = existingStudent.Id,
                    Name = existingStudent.Name,
                    Surname = existingStudent.Surname,
                    Email = existingStudent.Email,
                    Signed = existingStudent.Signed
                };

                return customerDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateStudentAsync(Guid id, Student student)
        {
            try
            {
                var existingStudent = await GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    _logger.LogWarning("Update fallito: studente con id {Id} non trovato", id);
                    return false;
                }

                existingStudent.Name = student.Name;
                existingStudent.Surname = student.Surname;
                existingStudent.Email = student.Email;
                _logger.LogInformation("Aggiornamento studente {Id}: Name={Name}, Surname={Surname}, Email={Email}",
           id, student.Name, student.Surname, student.Email);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex, "Errore durante l'aggiornamento dello studente con id {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            try
            {
                var existingStudent = await GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return false;
                }

                _context.Students.Remove(existingStudent);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

    }
}
