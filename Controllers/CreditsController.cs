using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.CreditFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.CreditsInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditsController : ControllerBase
    {
        private readonly ILogger<CreditsController> _logger;
        private readonly ICreditService _creditService;

        public CreditsController(ILogger<CreditsController> logger, ICreditService creditService)
        {
            _logger = logger;
            _creditService = creditService;
        }

        /// <summary>Получить все зачеты</summary>
        [HttpGet]
        public async Task<IActionResult> GetCreditsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetCredits was called");

            var credits = await _creditService.GetCreditsAsync(cancellationToken);

            return Ok(credits);
        }

        /// <summary>Получить зачет по id</summary>
        [HttpGet("{creditId:int}")]
        public async Task<IActionResult> GetCreditByIdAsync(int creditId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetCreditById was called");

            var credit = await _creditService.GetCreditByIdAsync(creditId, cancellationToken);

            return Ok(credit);
        }

        /// <summary>Зачеты конкретного студента (можно по одной дисциплине)</summary>
        [HttpPost("student")]
        public async Task<IActionResult> GetCreditsByStudentAsync(CreditStudentFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetCreditsByStudent was called");

            var credits = await _creditService.GetCreditsByStudentAsync(filter, cancellationToken);

            return Ok(credits);
        }

        /// <summary>Добавить зачет студенту</summary>
        [HttpPost]
        public async Task<IActionResult> AddCreditAsync(CreditRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddCredit was called");

            var credit = await _creditService.AddCreditAsync(request, cancellationToken);

            return Ok(credit);
        }

        /// <summary>Изменить зачет студента</summary>
        [HttpPut("{creditId:int}")]
        public async Task<IActionResult> UpdateCreditAsync(int creditId, CreditRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateCredit was called");

            var credit = await _creditService.UpdateCreditAsync(creditId, request, cancellationToken);

            return Ok(credit);
        }

        /// <summary>Удалить зачет</summary>
        [HttpDelete("{creditId:int}")]
        public async Task<IActionResult> DeleteCreditAsync(int creditId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteCredit was called");

            await _creditService.DeleteCreditAsync(creditId, cancellationToken);

            return NoContent();
        }
    }
}
