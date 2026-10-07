namespace Nikita_Yarancev_kt_31_23.Filters.CreditFilters
{
    public class CreditStudentFilter
    {
        /// <summary>Идентификатор студента</summary>
        public int StudentId { get; set; }

        /// <summary>Идентификатор дисциплины (необязательно)</summary>
        public int? DisciplineId { get; set; }
    }
}
