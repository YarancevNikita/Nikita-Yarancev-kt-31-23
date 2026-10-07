using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.SpecialtyFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Interfaces.SpecialtiesInterfaces
{
    public interface ISpecialtyService
    {
        public Task<Specialty[]> GetSpecialtiesAsync(CancellationToken cancellationToken);

        public Task<Specialty> GetSpecialtyByIdAsync(int specialtyId, CancellationToken cancellationToken);

        public Task<Specialty[]> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken);

        public Task<Specialty> AddSpecialtyAsync(SpecialtyRequest request, CancellationToken cancellationToken);

        public Task<Specialty> UpdateSpecialtyAsync(int specialtyId, SpecialtyRequest request, CancellationToken cancellationToken);

        public Task DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken);
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
                .OrderBy(o => o.Code)
                .ToArrayAsync(cancellationToken);

            return specialties;
        }

        public async Task<Specialty> GetSpecialtyByIdAsync(int specialtyId, CancellationToken cancellationToken = default)
        {
            var specialty = await _dbContext.Set<Specialty>()
                .FirstOrDefaultAsync(w => w.SpecialtyId == specialtyId, cancellationToken)
                ?? throw new KeyNotFoundException($"Специальность с id {specialtyId} не найдена");

            return specialty;
        }

        public async Task<Specialty[]> GetSpecialtiesByCodeAsync(SpecialtyCodeFilter filter, CancellationToken cancellationToken = default)
        {
            var specialties = await _dbContext.Set<Specialty>()
                .Where(w => w.Code == filter.Code)
                .ToArrayAsync(cancellationToken);

            return specialties;
        }

        public async Task<Specialty> AddSpecialtyAsync(SpecialtyRequest request, CancellationToken cancellationToken = default)
        {
            var specialty = new Specialty
            {
                Title = request.Title,
                Code = request.Code
            };

            _dbContext.Set<Specialty>().Add(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return specialty;
        }

        public async Task<Specialty> UpdateSpecialtyAsync(int specialtyId, SpecialtyRequest request, CancellationToken cancellationToken = default)
        {
            var specialty = await GetSpecialtyByIdAsync(specialtyId, cancellationToken);

            specialty.Title = request.Title;
            specialty.Code = request.Code;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return specialty;
        }

        public async Task DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default)
        {
            var specialty = await GetSpecialtyByIdAsync(specialtyId, cancellationToken);

            //Специальность удаляется физически, поэтому запрещаем удаление, если к ней привязаны группы
            var hasGroups = await _dbContext.Set<Group>()
                .AnyAsync(w => w.SpecialtyId == specialtyId, cancellationToken);

            if (hasGroups)
            {
                throw new ArgumentException("Нельзя удалить специальность, к которой привязаны группы");
            }

            _dbContext.Set<Specialty>().Remove(specialty);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
