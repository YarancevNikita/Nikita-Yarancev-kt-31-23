using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Filters.DisciplineFilters
{
    //Все параметры необязательные: если параметр не указан (null), фильтрация по нему не выполняется
    public class DisciplineFilter
    {
        /// <summary>Направление: Humanitarian - гуманитарное, Technical - техническое</summary>
        public DisciplineDirection? Direction { get; set; }

        /// <summary>Статус удаления: true - удаленные, false - не удаленные, null - все</summary>
        public bool? IsDeleted { get; set; }
    }
}
