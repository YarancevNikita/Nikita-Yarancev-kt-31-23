using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.GroupFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.GroupsInterfaces;

namespace Nikita_Yarancev_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet(Name = "GetGroups")]
        public async Task<IActionResult> GetGroupsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroups was called");

            var groups = await _groupService.GetGroupsAsync(cancellationToken);

            return Ok(groups);
        }

        [HttpPost(Name = "GetGroupsBySpecialty")]
        public async Task<IActionResult> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroupsBySpecialty was called");

            var groups = await _groupService.GetGroupsBySpecialtyAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
