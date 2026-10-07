using System.ComponentModel.DataAnnotations;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения дисциплины
    public class DisciplineRequest
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Направление: Humanitarian - гуманитарное, Technical - техническое</summary>
        [EnumDataType(typeof(DisciplineDirection))]
        public DisciplineDirection Direction { get; set; }
    }
}
