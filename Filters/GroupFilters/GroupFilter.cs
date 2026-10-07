namespace Nikita_Yarancev_kt_31_23.Filters.GroupFilters
{
    //Все параметры необязательные: если параметр не указан (null), фильтрация по нему не выполняется
    public class GroupFilter
    {
        /// <summary>Идентификатор специальности</summary>
        public int? SpecialtyId { get; set; }

        /// <summary>Год набора группы</summary>
        public int? Year { get; set; }

        /// <summary>Статус удаления: true - удаленные, false - не удаленные, null - все</summary>
        public bool? IsDeleted { get; set; }
    }
}
