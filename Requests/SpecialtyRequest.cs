using System.ComponentModel.DataAnnotations;

namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения специальности
    public class SpecialtyRequest
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Code { get; set; } = string.Empty;
    }
}
