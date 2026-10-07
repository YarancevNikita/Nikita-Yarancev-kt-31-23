using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.StudentFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.StudentsInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        /// <summary>Получить всех студентов (включая удаленных)</summary>
        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetStudents was called");

            var students = await _studentService.GetStudentsAsync(cancellationToken);

            return Ok(students);
        }

        /// <summary>Получить студента по id</summary>
        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetStudentById was called");

            var student = await _studentService.GetStudentByIdAsync(studentId, cancellationToken);

            return Ok(student);
        }

        /// <summary>Получить студентов с фильтрацией по группе, ФИО и статусу удаления</summary>
        [HttpPost("filter")]
        public async Task<IActionResult> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetStudentsByFilter was called");

            var students = await _studentService.GetStudentsByFilterAsync(filter, cancellationToken);

            return Ok(students);
        }

        /// <summary>Добавить студента</summary>
        [HttpPost]
        public async Task<IActionResult> AddStudentAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddStudent was called");

            var student = await _studentService.AddStudentAsync(request, cancellationToken);

            return Ok(student);
        }

        /// <summary>Изменить студента</summary>
        [HttpPut("{studentId:int}")]
        public async Task<IActionResult> UpdateStudentAsync(int studentId, StudentRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateStudent was called");

            var student = await _studentService.UpdateStudentAsync(studentId, request, cancellationToken);

            return Ok(student);
        }

        /// <summary>Удалить студента (логически)</summary>
        [HttpDelete("{studentId:int}")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteStudent was called");

            await _studentService.DeleteStudentAsync(studentId, cancellationToken);

            return NoContent();
        }

        /// <summary>Восстановить удаленного студента</summary>
        [HttpPut("{studentId:int}/restore")]
        public async Task<IActionResult> RestoreStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method RestoreStudent was called");

            var student = await _studentService.RestoreStudentAsync(studentId, cancellationToken);

            return Ok(student);
        }
    }
}
