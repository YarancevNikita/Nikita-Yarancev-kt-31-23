using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.CreditFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Interfaces.CreditsInterfaces
{
    public interface ICreditService
    {
        public Task<Credit[]> GetCreditsAsync(CancellationToken cancellationToken);

        public Task<Credit> GetCreditByIdAsync(int creditId, CancellationToken cancellationToken);

        public Task<Credit[]> GetCreditsByStudentAsync(CreditStudentFilter filter, CancellationToken cancellationToken);

        public Task<Credit> AddCreditAsync(CreditRequest request, CancellationToken cancellationToken);

        public Task<Credit> UpdateCreditAsync(int creditId, CreditRequest request, CancellationToken cancellationToken);

        public Task DeleteCreditAsync(int creditId, CancellationToken cancellationToken);
    }

    public class CreditService : ICreditService
    {
        private readonly StudentDbContext _dbContext;

        public CreditService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Credit[]> GetCreditsAsync(CancellationToken cancellationToken = default)
        {
            var credits = await _dbContext.Set<Credit>()
                .OrderBy(o => o.Date)
                .ToArrayAsync(cancellationToken);

            return credits;
        }

        public async Task<Credit> GetCreditByIdAsync(int creditId, CancellationToken cancellationToken = default)
        {
            var credit = await _dbContext.Set<Credit>()
                .FirstOrDefaultAsync(w => w.CreditId == creditId, cancellationToken)
                ?? throw new KeyNotFoundException($"Зачет с id {creditId} не найден");

            return credit;
        }

        public async Task<Credit[]> GetCreditsByStudentAsync(CreditStudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Credit>()
                .Where(w => w.StudentId == filter.StudentId);

            //Если указана дисциплина, дополнительно фильтруем по ней
            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(w => w.DisciplineId == filter.DisciplineId.Value);
            }

            var credits = await query
                .OrderBy(o => o.Date)
                .ToArrayAsync(cancellationToken);

            return credits;
        }

        public async Task<Credit> AddCreditAsync(CreditRequest request, CancellationToken cancellationToken = default)
        {
            var credit = new Credit
            {
                IsPassed = request.IsPassed,
                Date = request.Date,
                Student = await GetActiveStudentAsync(request.StudentId, cancellationToken),
                Discipline = await GetActiveDisciplineAsync(request.DisciplineId, cancellationToken)
            };

            _dbContext.Set<Credit>().Add(credit);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return credit;
        }

        public async Task<Credit> UpdateCreditAsync(int creditId, CreditRequest request, CancellationToken cancellationToken = default)
        {
            var credit = await GetCreditByIdAsync(creditId, cancellationToken);

            credit.IsPassed = request.IsPassed;
            credit.Date = request.Date;
            credit.Student = await GetActiveStudentAsync(request.StudentId, cancellationToken);
            credit.Discipline = await GetActiveDisciplineAsync(request.DisciplineId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return credit;
        }

        public async Task DeleteCreditAsync(int creditId, CancellationToken cancellationToken = default)
        {
            var credit = await GetCreditByIdAsync(creditId, cancellationToken);

            _dbContext.Set<Credit>().Remove(credit);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<Student> GetActiveStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Set<Student>()
                .FirstOrDefaultAsync(w => w.StudentId == studentId, cancellationToken)
                ?? throw new KeyNotFoundException($"Студент с id {studentId} не найден");

            if (student.IsDeleted)
            {
                throw new ArgumentException($"Студент с id {studentId} удален");
            }

            return student;
        }

        private async Task<Discipline> GetActiveDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Set<Discipline>()
                .FirstOrDefaultAsync(w => w.DisciplineId == disciplineId, cancellationToken)
                ?? throw new KeyNotFoundException($"Дисциплина с id {disciplineId} не найдена");

            if (discipline.IsDeleted)
            {
                throw new ArgumentException($"Дисциплина с id {disciplineId} удалена");
            }

            return discipline;
        }
    }
}
