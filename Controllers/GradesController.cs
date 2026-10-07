using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.GradeFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.GradesInterfaces;

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

        [HttpGet(Name = "GetGrades")]
        public async Task<IActionResult> GetGradesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGrades was called");

            var grades = await _gradeService.GetGradesAsync(cancellationToken);

            return Ok(grades);
        }

        [HttpPost(Name = "GetGradesByStudent")]
        public async Task<IActionResult> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGradesByStudent was called");

            var grades = await _gradeService.GetGradesByStudentAsync(filter, cancellationToken);

            return Ok(grades);
        }
    }
}
