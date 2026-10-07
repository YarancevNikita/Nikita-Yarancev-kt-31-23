using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.StudentFilters;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsAsync(CancellationToken cancellationToken);

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student[]> GetStudentsAsync(CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Student>()
                .Where(w => !w.IsDeleted)
                .ToArrayAsync(cancellationToken);

            return students;
        }

        public async Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Student>()
                .Where(w => w.Group.Name == filter.GroupName)
                .ToArrayAsync(cancellationToken);

            return students;
        }
    }
}
