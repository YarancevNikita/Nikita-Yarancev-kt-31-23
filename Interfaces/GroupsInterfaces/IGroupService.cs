using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.GroupFilters;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsAsync(CancellationToken cancellationToken);

        public Task<Group[]> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group[]> GetGroupsAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _dbContext.Set<Group>()
                .Where(w => !w.IsDeleted)
                .ToArrayAsync(cancellationToken);

            return groups;
        }

        public async Task<Group[]> GetGroupsBySpecialtyAsync(GroupSpecialtyFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _dbContext.Set<Group>()
                .Where(w => !w.IsDeleted && w.Specialty.Code == filter.SpecialtyCode)
                .ToArrayAsync(cancellationToken);

            return groups;
        }
    }
}
