using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.GroupFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsAsync(CancellationToken cancellationToken);

        public Task<Group> GetGroupByIdAsync(int groupId, CancellationToken cancellationToken);

        public Task<Group[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken);

        public Task<Group> AddGroupAsync(GroupRequest request, CancellationToken cancellationToken);

        public Task<Group> UpdateGroupAsync(int groupId, GroupRequest request, CancellationToken cancellationToken);

        public Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken);

        public Task<Group> RestoreGroupAsync(int groupId, CancellationToken cancellationToken);
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
                .OrderBy(o => o.Name)
                .ToArrayAsync(cancellationToken);

            return groups;
        }

        public async Task<Group> GetGroupByIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Set<Group>()
                .FirstOrDefaultAsync(w => w.GroupId == groupId, cancellationToken)
                ?? throw new KeyNotFoundException($"Группа с id {groupId} не найдена");

            return group;
        }

        public async Task<Group[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Group>().AsQueryable();

            //Каждый параметр фильтра применяется, только если он указан
            if (filter.SpecialtyId.HasValue)
            {
                query = query.Where(w => w.SpecialtyId == filter.SpecialtyId.Value);
            }

            if (filter.Year.HasValue)
            {
                query = query.Where(w => w.Year == filter.Year.Value);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(w => w.IsDeleted == filter.IsDeleted.Value);
            }

            var groups = await query
                .OrderBy(o => o.Name)
                .ToArrayAsync(cancellationToken);

            return groups;
        }

        public async Task<Group> AddGroupAsync(GroupRequest request, CancellationToken cancellationToken = default)
        {
            var specialty = await GetSpecialtyAsync(request.SpecialtyId, cancellationToken);

            var group = new Group
            {
                Name = request.Name,
                Course = request.Course,
                Year = request.Year,
                Specialty = specialty
            };

            _dbContext.Set<Group>().Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return group;
        }

        public async Task<Group> UpdateGroupAsync(int groupId, GroupRequest request, CancellationToken cancellationToken = default)
        {
            var group = await GetGroupByIdAsync(groupId, cancellationToken);

            if (group.IsDeleted)
            {
                throw new ArgumentException("Группа удалена, сначала восстановите её");
            }

            group.Name = request.Name;
            group.Course = request.Course;
            group.Year = request.Year;
            group.Specialty = await GetSpecialtyAsync(request.SpecialtyId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return group;
        }

        public async Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var group = await GetGroupByIdAsync(groupId, cancellationToken);

            if (group.IsDeleted)
            {
                throw new ArgumentException("Группа уже удалена");
            }

            //Удаление логическое: ставим признак удаления группе и всем её студентам
            var students = await _dbContext.Set<Student>()
                .Where(w => w.GroupId == groupId && !w.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var student in students)
            {
                student.IsDeleted = true;
            }

            group.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Group> RestoreGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var group = await GetGroupByIdAsync(groupId, cancellationToken);

            if (!group.IsDeleted)
            {
                throw new ArgumentException("Группа не удалена");
            }

            group.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return group;
        }

        private async Task<Specialty> GetSpecialtyAsync(int specialtyId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Specialty>()
                .FirstOrDefaultAsync(w => w.SpecialtyId == specialtyId, cancellationToken)
                ?? throw new KeyNotFoundException($"Специальность с id {specialtyId} не найдена");
        }
    }
}
