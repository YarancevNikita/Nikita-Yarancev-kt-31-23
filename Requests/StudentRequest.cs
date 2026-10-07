using System.ComponentModel.DataAnnotations;

namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения студента
    public class StudentRequest
    {
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        public int GroupId { get; set; }
    }
}
