using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.DisciplineFilters;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        public Task<Discipline[]> GetDisciplinesAsync(CancellationToken cancellationToken);

        public Task<Discipline[]> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken);
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
                .Where(w => !w.IsDeleted)
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }

        public async Task<Discipline[]> GetDisciplinesByNameAsync(DisciplineNameFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _dbContext.Set<Discipline>()
                .Where(w => !w.IsDeleted && w.Name == filter.Name)
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }
    }
}
