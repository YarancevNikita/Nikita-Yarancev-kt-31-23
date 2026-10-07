using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.GradeFilters;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Interfaces.GradesInterfaces
{
    public interface IGradeService
    {
        public Task<Grade[]> GetGradesAsync(CancellationToken cancellationToken);

        public Task<Grade[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;

        public GradeService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Grade[]> GetGradesAsync(CancellationToken cancellationToken = default)
        {
            var grades = await _dbContext.Set<Grade>()
                .ToArrayAsync(cancellationToken);

            return grades;
        }

        public async Task<Grade[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Grade>()
                .Where(w => w.StudentId == filter.StudentId);

            //Если указана дисциплина, дополнительно фильтруем по ней
            if (!string.IsNullOrEmpty(filter.DisciplineName))
            {
                query = query.Where(w => w.Discipline.Name == filter.DisciplineName);
            }

            var grades = await query.ToArrayAsync(cancellationToken);

            return grades;
        }
    }
}
