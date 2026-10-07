using System.ComponentModel.DataAnnotations;

namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения группы
    public class GroupRequest
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Курс обучения (1-6)</summary>
        [Range(1, 6)]
        public int Course { get; set; }

        /// <summary>Год набора группы</summary>
        [Range(1900, 2100)]
        public int Year { get; set; }

        public int SpecialtyId { get; set; }
    }
}
