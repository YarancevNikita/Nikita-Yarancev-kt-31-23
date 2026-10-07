using Microsoft.AspNetCore.Mvc;
using Nikita_Yarancev_kt_31_23.Filters.GroupFilters;
using Nikita_Yarancev_kt_31_23.Interfaces.GroupsInterfaces;
using Nikita_Yarancev_kt_31_23.Requests;

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

        /// <summary>Получить все группы (включая удаленные)</summary>
        [HttpGet]
        public async Task<IActionResult> GetGroupsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroups was called");

            var groups = await _groupService.GetGroupsAsync(cancellationToken);

            return Ok(groups);
        }

        /// <summary>Получить группу по id</summary>
        [HttpGet("{groupId:int}")]
        public async Task<IActionResult> GetGroupByIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroupById was called");

            var group = await _groupService.GetGroupByIdAsync(groupId, cancellationToken);

            return Ok(group);
        }

        /// <summary>Получить группы с фильтрацией по специальности, году набора и статусу удаления</summary>
        [HttpPost("filter")]
        public async Task<IActionResult> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method GetGroupsByFilter was called");

            var groups = await _groupService.GetGroupsByFilterAsync(filter, cancellationToken);

            return Ok(groups);
        }

        /// <summary>Добавить группу</summary>
        [HttpPost]
        public async Task<IActionResult> AddGroupAsync(GroupRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method AddGroup was called");

            var group = await _groupService.AddGroupAsync(request, cancellationToken);

            return Ok(group);
        }

        /// <summary>Изменить группу</summary>
        [HttpPut("{groupId:int}")]
        public async Task<IActionResult> UpdateGroupAsync(int groupId, GroupRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method UpdateGroup was called");

            var group = await _groupService.UpdateGroupAsync(groupId, request, cancellationToken);

            return Ok(group);
        }

        /// <summary>Удалить группу (логически, вместе со всеми её студентами)</summary>
        [HttpDelete("{groupId:int}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method DeleteGroup was called");

            await _groupService.DeleteGroupAsync(groupId, cancellationToken);

            return NoContent();
        }

        /// <summary>Восстановить удаленную группу (студенты восстанавливаются отдельно)</summary>
        [HttpPut("{groupId:int}/restore")]
        public async Task<IActionResult> RestoreGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            _logger.LogError("Method RestoreGroup was called");

            var group = await _groupService.RestoreGroupAsync(groupId, cancellationToken);

            return Ok(group);
        }
    }
}
