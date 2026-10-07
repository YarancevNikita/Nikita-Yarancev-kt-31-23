namespace Nikita_Yarancev_kt_31_23.Filters.GradeFilters
{
    public class GradeYearFilter
    {
        /// <summary>Год, за который считается средний балл (по дате оценки)</summary>
        public int Year { get; set; }

        /// <summary>Идентификатор группы (необязательно)</summary>
        public int? GroupId { get; set; }

        /// <summary>Идентификатор дисциплины (необязательно)</summary>
        public int? DisciplineId { get; set; }
    }
}
