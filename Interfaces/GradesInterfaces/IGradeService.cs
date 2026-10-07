using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database;
using Nikita_Yarancev_kt_31_23.Filters.GradeFilters;
using Nikita_Yarancev_kt_31_23.Models;
using Nikita_Yarancev_kt_31_23.Requests;
using Nikita_Yarancev_kt_31_23.Responses;

namespace Nikita_Yarancev_kt_31_23.Interfaces.GradesInterfaces
{
    public interface IGradeService
    {
        public Task<Grade[]> GetGradesAsync(CancellationToken cancellationToken);

        public Task<Grade> GetGradeByIdAsync(int gradeId, CancellationToken cancellationToken);

        public Task<Grade[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken);

        public Task<GroupDisciplineAverageResponse> GetGroupDisciplineAverageAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken);

        public Task<YearAverageResponse> GetYearAverageAsync(GradeYearFilter filter, CancellationToken cancellationToken);

        public Task<Grade> AddGradeAsync(GradeRequest request, CancellationToken cancellationToken);

        public Task<Grade> UpdateGradeAsync(int gradeId, GradeRequest request, CancellationToken cancellationToken);

        public Task DeleteGradeAsync(int gradeId, CancellationToken cancellationToken);
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
                .OrderBy(o => o.Date)
                .ToArrayAsync(cancellationToken);

            return grades;
        }

        public async Task<Grade> GetGradeByIdAsync(int gradeId, CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Set<Grade>()
                .FirstOrDefaultAsync(w => w.GradeId == gradeId, cancellationToken)
                ?? throw new KeyNotFoundException($"Оценка с id {gradeId} не найдена");

            return grade;
        }

        public async Task<Grade[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Grade>()
                .Where(w => w.StudentId == filter.StudentId);

            //Если указана дисциплина, дополнительно фильтруем по ней
            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(w => w.DisciplineId == filter.DisciplineId.Value);
            }

            var grades = await query
                .OrderBy(o => o.Date)
                .ToArrayAsync(cancellationToken);

            return grades;
        }

        public async Task<GroupDisciplineAverageResponse> GetGroupDisciplineAverageAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Set<Group>()
                .FirstOrDefaultAsync(w => w.GroupId == filter.GroupId, cancellationToken)
                ?? throw new KeyNotFoundException($"Группа с id {filter.GroupId} не найдена");

            var discipline = await _dbContext.Set<Discipline>()
                .FirstOrDefaultAsync(w => w.DisciplineId == filter.DisciplineId, cancellationToken)
                ?? throw new KeyNotFoundException($"Дисциплина с id {filter.DisciplineId} не найдена");

            //Учитываем только оценки не удаленных студентов этой группы
            var query = _dbContext.Set<Grade>()
                .Where(w => w.Student.GroupId == filter.GroupId
                    && !w.Student.IsDeleted
                    && w.DisciplineId == filter.DisciplineId);

            var average = await query.AverageAsync(a => (double?)a.Value, cancellationToken);

            return new GroupDisciplineAverageResponse
            {
                GroupId = group.GroupId,
                GroupName = group.Name,
                DisciplineId = discipline.DisciplineId,
                DisciplineName = discipline.Name,
                AverageGrade = average.HasValue ? Math.Round(average.Value, 2) : null,
                GradesCount = await query.CountAsync(cancellationToken)
            };
        }

        public async Task<YearAverageResponse> GetYearAverageAsync(GradeYearFilter filter, CancellationToken cancellationToken = default)
        {
            if (filter.Year < 1 || filter.Year > 9999)
            {
                throw new ArgumentException("Некорректный год");
            }

            var yearStart = new DateOnly(filter.Year, 1, 1);
            var yearEnd = new DateOnly(filter.Year, 12, 31);

            var query = _dbContext.Set<Grade>()
                .Where(w => w.Date >= yearStart && w.Date <= yearEnd && !w.Student.IsDeleted);

            //Группа и дисциплина необязательные
            if (filter.GroupId.HasValue)
            {
                query = query.Where(w => w.Student.GroupId == filter.GroupId.Value);
            }

            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(w => w.DisciplineId == filter.DisciplineId.Value);
            }

            var average = await query.AverageAsync(a => (double?)a.Value, cancellationToken);

            return new YearAverageResponse
            {
                Year = filter.Year,
                GroupId = filter.GroupId,
                DisciplineId = filter.DisciplineId,
                AverageGrade = average.HasValue ? Math.Round(average.Value, 2) : null,
                GradesCount = await query.CountAsync(cancellationToken)
            };
        }

        public async Task<Grade> AddGradeAsync(GradeRequest request, CancellationToken cancellationToken = default)
        {
            var grade = new Grade
            {
                Value = request.Value,
                Date = request.Date,
                Student = await GetActiveStudentAsync(request.StudentId, cancellationToken),
                Discipline = await GetActiveDisciplineAsync(request.DisciplineId, cancellationToken)
            };

            _dbContext.Set<Grade>().Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return grade;
        }

        public async Task<Grade> UpdateGradeAsync(int gradeId, GradeRequest request, CancellationToken cancellationToken = default)
        {
            var grade = await GetGradeByIdAsync(gradeId, cancellationToken);

            grade.Value = request.Value;
            grade.Date = request.Date;
            grade.Student = await GetActiveStudentAsync(request.StudentId, cancellationToken);
            grade.Discipline = await GetActiveDisciplineAsync(request.DisciplineId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return grade;
        }

        public async Task DeleteGradeAsync(int gradeId, CancellationToken cancellationToken = default)
        {
            var grade = await GetGradeByIdAsync(gradeId, cancellationToken);

            _dbContext.Set<Grade>().Remove(grade);
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
