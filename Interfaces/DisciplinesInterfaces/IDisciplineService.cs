using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.DisciplineFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        public Task<Discipline[]> GetDisciplinesAsync(CancellationToken cancellationToken);

        public Task<Discipline> GetDisciplineByIdAsync(int disciplineId, CancellationToken cancellationToken);

        public Task<Discipline[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken);

        public Task<Discipline> AddDisciplineAsync(DisciplineRequest request, CancellationToken cancellationToken);

        public Task<Discipline> UpdateDisciplineAsync(int disciplineId, DisciplineRequest request, CancellationToken cancellationToken);

        public Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken);

        public Task<Discipline> RestoreDisciplineAsync(int disciplineId, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplineService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discipline[]> GetDisciplinesAsync(CancellationToken cancellationToken = default)
        {
            var disciplines = await _dbContext.Set<Discipline>()
                .OrderBy(o => o.Name)
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }

        public async Task<Discipline> GetDisciplineByIdAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var discipline = await _dbContext.Set<Discipline>()
                .FirstOrDefaultAsync(w => w.DisciplineId == disciplineId, cancellationToken)
                ?? throw new KeyNotFoundException($"Дисциплина с id {disciplineId} не найдена");

            return discipline;
        }

        public async Task<Discipline[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Discipline>().AsQueryable();

            //Каждый параметр фильтра применяется, только если он указан
            if (filter.Direction.HasValue)
            {
                query = query.Where(w => w.Direction == filter.Direction.Value);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(w => w.IsDeleted == filter.IsDeleted.Value);
            }

            var disciplines = await query
                .OrderBy(o => o.Name)
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }

        public async Task<Discipline> AddDisciplineAsync(DisciplineRequest request, CancellationToken cancellationToken = default)
        {
            var discipline = new Discipline
            {
                Name = request.Name,
                Direction = request.Direction
            };

            _dbContext.Set<Discipline>().Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return discipline;
        }

        public async Task<Discipline> UpdateDisciplineAsync(int disciplineId, DisciplineRequest request, CancellationToken cancellationToken = default)
        {
            var discipline = await GetDisciplineByIdAsync(disciplineId, cancellationToken);

            if (discipline.IsDeleted)
            {
                throw new ArgumentException("Дисциплина удалена, сначала восстановите её");
            }

            discipline.Name = request.Name;
            discipline.Direction = request.Direction;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return discipline;
        }

        public async Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var discipline = await GetDisciplineByIdAsync(disciplineId, cancellationToken);

            if (discipline.IsDeleted)
            {
                throw new ArgumentException("Дисциплина уже удалена");
            }

            //Удаление логическое: только ставим признак удаления
            discipline.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Discipline> RestoreDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {
            var discipline = await GetDisciplineByIdAsync(disciplineId, cancellationToken);

            if (!discipline.IsDeleted)
            {
                throw new ArgumentException("Дисциплина не удалена");
            }

            discipline.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return discipline;
        }
    }
}
