namespace Nikita_Yarancev_kt_31_23.Filters.StudentFilters
{
    //Все параметры необязательные: если параметр не указан (null), фильтрация по нему не выполняется
    public class StudentFilter
    {
        /// <summary>Название группы</summary>
        public string? GroupName { get; set; }

        /// <summary>ФИО целиком или частично, например "Иванов Иван" (без учета регистра)</summary>
        public string? Fio { get; set; }

        /// <summary>Статус удаления: true - удаленные, false - не удаленные, null - все</summary>
        public bool? IsDeleted { get; set; }
    }
}
