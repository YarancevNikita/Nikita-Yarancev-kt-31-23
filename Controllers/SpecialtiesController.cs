using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.SpecialtyFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.SpecialtiesInterfaces;

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

        [HttpGet(Name = "GetSpecialties")]
        public async Task<IActionResult> GetSpecialtiesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetSpecialties was called");

            var specialties = await _specialtyService.GetSpecialtiesAsync(cancellationToken);

            return Ok(specialties);
        }

        [HttpPost(Name = "GetSpecialtiesByCode")]
        public async Task<IActionResult> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetSpecialtiesByCode was called");

            var specialties = await _specialtyService.GetSpecialtiesByCodeAsync(filter, cancellationToken);

            return Ok(specialties);
        }
    }
}
