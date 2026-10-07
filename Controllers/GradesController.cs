using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.GradeFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.GradesInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly ILogger<GradesController> _logger;
        private readonly IGradeService _gradeService;

        public GradesController(ILogger<GradesController> logger, IGradeService gradeService)
        {
            _logger = logger;
            _gradeService = gradeService;
        }

        /// <summary>Получить все оценки</summary>
        [HttpGet]
        public async Task<IActionResult> GetGradesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGrades was called");

            var grades = await _gradeService.GetGradesAsync(cancellationToken);

            return Ok(grades);
        }

        /// <summary>Получить оценку по id</summary>
        [HttpGet("{gradeId:int}")]
        public async Task<IActionResult> GetGradeByIdAsync(int gradeId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGradeById was called");

            var grade = await _gradeService.GetGradeByIdAsync(gradeId, cancellationToken);

            return Ok(grade);
        }

        /// <summary>Оценки конкретного студента (можно по одной дисциплине)</summary>
        [HttpPost("student")]
        public async Task<IActionResult> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGradesByStudent was called");

            var grades = await _gradeService.GetGradesByStudentAsync(filter, cancellationToken);

            return Ok(grades);
        }

        /// <summary>Средний балл по предмету в группе</summary>
        [HttpPost("average/group-discipline")]
        public async Task<IActionResult> GetGroupDisciplineAverageAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroupDisciplineAverage was called");

            var average = await _gradeService.GetGroupDisciplineAverageAsync(filter, cancellationToken);

            return Ok(average);
        }

        /// <summary>Средний балл за год (можно уточнить группой и/или дисциплиной)</summary>
        [HttpPost("average/year")]
        public async Task<IActionResult> GetYearAverageAsync(GradeYearFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetYearAverage was called");

            var average = await _gradeService.GetYearAverageAsync(filter, cancellationToken);

            return Ok(average);
        }

        /// <summary>Добавить оценку студенту</summary>
        [HttpPost]
        public async Task<IActionResult> AddGradeAsync(GradeRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddGrade was called");

            var grade = await _gradeService.AddGradeAsync(request, cancellationToken);

            return Ok(grade);
        }

        /// <summary>Изменить оценку студента</summary>
        [HttpPut("{gradeId:int}")]
        public async Task<IActionResult> UpdateGradeAsync(int gradeId, GradeRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateGrade was called");

            var grade = await _gradeService.UpdateGradeAsync(gradeId, request, cancellationToken);

            return Ok(grade);
        }

        /// <summary>Удалить оценку</summary>
        [HttpDelete("{gradeId:int}")]
        public async Task<IActionResult> DeleteGradeAsync(int gradeId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteGrade was called");

            await _gradeService.DeleteGradeAsync(gradeId, cancellationToken);

            return NoContent();
        }
    }
}
