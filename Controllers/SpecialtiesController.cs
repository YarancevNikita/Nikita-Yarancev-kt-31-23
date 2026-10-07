using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.SpecialtyFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.SpecialtiesInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ILogger<SpecialtiesController> _logger;
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ILogger<SpecialtiesController> logger, ISpecialtyService specialtyService)
        {
            _logger = logger;
            _specialtyService = specialtyService;
        }

        /// <summary>Получить все специальности</summary>
        [HttpGet]
        public async Task<IActionResult> GetSpecialtiesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetSpecialties was called");

            var specialties = await _specialtyService.GetSpecialtiesAsync(cancellationToken);

            return Ok(specialties);
        }

        /// <summary>Получить специальность по id</summary>
        [HttpGet("{specialtyId:int}")]
        public async Task<IActionResult> GetSpecialtyByIdAsync(int specialtyId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetSpecialtyById was called");

            var specialty = await _specialtyService.GetSpecialtyByIdAsync(specialtyId, cancellationToken);

            return Ok(specialty);
        }

        /// <summary>Получить специальности по коду</summary>
        [HttpPost("filter")]
        public async Task<IActionResult> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetSpecialtiesByCode was called");

            var specialties = await _specialtyService.GetSpecialtiesByCodeAsync(filter, cancellationToken);

            return Ok(specialties);
        }

        /// <summary>Добавить специальность</summary>
        [HttpPost]
        public async Task<IActionResult> AddSpecialtyAsync(SpecialtyRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddSpecialty was called");

            var specialty = await _specialtyService.AddSpecialtyAsync(request, cancellationToken);

            return Ok(specialty);
        }

        /// <summary>Изменить специальность</summary>
        [HttpPut("{specialtyId:int}")]
        public async Task<IActionResult> UpdateSpecialtyAsync(int specialtyId, SpecialtyRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateSpecialty was called");

            var specialty = await _specialtyService.UpdateSpecialtyAsync(specialtyId, request, cancellationToken);

            return Ok(specialty);
        }

        /// <summary>Удалить специальность (только если к ней не привязаны группы)</summary>
        [HttpDelete("{specialtyId:int}")]
        public async Task<IActionResult> DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteSpecialty was called");

            await _specialtyService.DeleteSpecialtyAsync(specialtyId, cancellationToken);

            return NoContent();
        }
    }
}
