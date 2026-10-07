using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.SpecialtyFilters;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Interfaces.SpecialtiesInterfaces
{
    public interface ISpecialtyService
    {
        public Task<Specialty[]> GetSpecialtiesAsync(CancellationToken cancellationToken);

        public Task<Specialty[]> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken);
    }

    public class SpecialtyService : ISpecialtyService
    {
        private readonly StudentDbContext _dbContext;

        public SpecialtyService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Specialty[]> GetSpecialtiesAsync(CancellationToken cancellationToken = default)
        {
            var specialties = await _dbContext.Set<Specialty>()
                .ToArrayAsync(cancellationToken);

            return specialties;
        }

        public async Task<Specialty[]> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            var specialties = await _dbContext.Set<Specialty>()
                .Where(w => w.Code == filter.Code)
                .ToArrayAsync(cancellationToken);

            return specialties;
        }
    }
}
