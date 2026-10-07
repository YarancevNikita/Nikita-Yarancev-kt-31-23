using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.StudentFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;

namespace Nikita_Yarancev_kt_31_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsAsync(CancellationToken cancellationToken);

        public Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken);

        public Task<Student[]> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken);

        public Task<Student> AddStudentAsync(StudentRequest request, CancellationToken cancellationToken);

        public Task<Student> UpdateStudentAsync(int studentId, StudentRequest request, CancellationToken cancellationToken);

        public Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken);

        public Task<Student> RestoreStudentAsync(int studentId, CancellationToken cancellationToken);
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
                .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                .ToArrayAsync(cancellationToken);

            return students;
        }

        public async Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Set<Student>()
                .FirstOrDefaultAsync(w => w.StudentId == studentId, cancellationToken)
                ?? throw new KeyNotFoundException($"Студент с id {studentId} не найден");

            return student;
        }

        public async Task<Student[]> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Student>().AsQueryable();

            //Каждый параметр фильтра применяется, только если он указан
            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                query = query.Where(w => w.Group.Name == filter.GroupName);
            }

            if (!string.IsNullOrWhiteSpace(filter.Fio))
            {
                //Каждое слово из ФИО должно совпадать с началом фамилии, имени или отчества (без учета регистра)
                foreach (var part in filter.Fio.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    var pattern = $"{part}%";

                    query = query.Where(w => EF.Functions.ILike(w.LastName, pattern)
                        || EF.Functions.ILike(w.FirstName, pattern)
                        || (w.MiddleName != null && EF.Functions.ILike(w.MiddleName, pattern)));
                }
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(w => w.IsDeleted == filter.IsDeleted.Value);
            }

            var students = await query
                .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                .ToArrayAsync(cancellationToken);

            return students;
        }

        public async Task<Student> AddStudentAsync(StudentRequest request, CancellationToken cancellationToken = default)
        {
            var group = await GetActiveGroupAsync(request.GroupId, cancellationToken);

            var student = new Student
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                Group = group
            };

            _dbContext.Set<Student>().Add(student);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return student;
        }

        public async Task<Student> UpdateStudentAsync(int studentId, StudentRequest request, CancellationToken cancellationToken = default)
        {
            var student = await GetStudentByIdAsync(studentId, cancellationToken);

            if (student.IsDeleted)
            {
                throw new ArgumentException("Студент удален, сначала восстановите его");
            }

            student.LastName = request.LastName;
            student.FirstName = request.FirstName;
            student.MiddleName = request.MiddleName;
            student.Group = await GetActiveGroupAsync(request.GroupId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return student;
        }

        public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await GetStudentByIdAsync(studentId, cancellationToken);

            if (student.IsDeleted)
            {
                throw new ArgumentException("Студент уже удален");
            }

            //Удаление логическое: только ставим признак удаления
            student.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Student> RestoreStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await GetStudentByIdAsync(studentId, cancellationToken);

            if (!student.IsDeleted)
            {
                throw new ArgumentException("Студент не удален");
            }

            if (student.Group.IsDeleted)
            {
                throw new ArgumentException("Группа студента удалена, сначала восстановите группу");
            }

            student.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return student;
        }

        private async Task<Group> GetActiveGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Set<Group>()
                .FirstOrDefaultAsync(w => w.GroupId == groupId, cancellationToken)
                ?? throw new KeyNotFoundException($"Группа с id {groupId} не найдена");

            if (group.IsDeleted)
            {
                throw new ArgumentException($"Группа с id {groupId} удалена");
            }

            return group;
        }
    }
}
