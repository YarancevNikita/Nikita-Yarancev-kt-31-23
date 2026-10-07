using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.DisciplineFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.DisciplinesInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

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

        /// <summary>Получить все дисциплины (включая удаленные)</summary>
        [HttpGet]
        public async Task<IActionResult> GetDisciplinesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetDisciplines was called");

            var disciplines = await _disciplineService.GetDisciplinesAsync(cancellationToken);

            return Ok(disciplines);
        }

        /// <summary>Получить дисциплину по id</summary>
        [HttpGet("{disciplineId:int}")]
        public async Task<IActionResult> GetDisciplineByIdAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetDisciplineById was called");

            var discipline = await _disciplineService.GetDisciplineByIdAsync(disciplineId, cancellationToken);

            return Ok(discipline);
        }

        /// <summary>Получить дисциплины с фильтрацией по направлению (гуманитарное/техническое) и статусу удаления</summary>
        [HttpPost("filter")]
        public async Task<IActionResult> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetDisciplinesByFilter was called");

            var disciplines = await _disciplineService.GetDisciplinesByFilterAsync(filter, cancellationToken);

            return Ok(disciplines);
        }

        /// <summary>Добавить дисциплину</summary>
        [HttpPost]
        public async Task<IActionResult> AddDisciplineAsync(DisciplineRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddDiscipline was called");

            var discipline = await _disciplineService.AddDisciplineAsync(request, cancellationToken);

            return Ok(discipline);
        }

        /// <summary>Изменить дисциплину</summary>
        [HttpPut("{disciplineId:int}")]
        public async Task<IActionResult> UpdateDisciplineAsync(int disciplineId, DisciplineRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateDiscipline was called");

            var discipline = await _disciplineService.UpdateDisciplineAsync(disciplineId, request, cancellationToken);

            return Ok(discipline);
        }

        /// <summary>Удалить дисциплину (логически)</summary>
        [HttpDelete("{disciplineId:int}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteDiscipline was called");

            await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);

            return NoContent();
        }

        /// <summary>Восстановить удаленную дисциплину</summary>
        [HttpPut("{disciplineId:int}/restore")]
        public async Task<IActionResult> RestoreDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method RestoreDiscipline was called");

            var discipline = await _disciplineService.RestoreDisciplineAsync(disciplineId, cancellationToken);

            return Ok(discipline);
        }
    }
}
