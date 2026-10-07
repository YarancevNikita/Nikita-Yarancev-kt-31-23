namespace Nikita_Yarancev_kt_31_23.Filters.GradeFilters
{
    public class GradeStudentFilter
    {
        public int StudentId { get; set; }

        //Необязательный параметр: если не указан, вернутся оценки по всем дисциплинам
        public string? DisciplineName { get; set; }
    }
}
