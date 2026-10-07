using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.DisciplineFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.DisciplinesInterfaces;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }

        [HttpGet(Name = "GetDisciplines")]
        public async Task<IActionResult> GetDisciplinesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetDisciplines was called");

            var disciplines = await _disciplineService.GetDisciplinesAsync(cancellationToken);

            return Ok(disciplines);
        }

        [HttpPost(Name = "GetDisciplinesByName")]
        public async Task<IActionResult> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetDisciplinesByName was called");

            var disciplines = await _disciplineService.GetDisciplinesByNameAsync(filter, cancellationToken);

            return Ok(disciplines);
        }
    }
}
