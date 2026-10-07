using System.ComponentModel.DataAnnotations;

namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения оценки
    public class GradeRequest
    {
        public int StudentId { get; set; }

        public int DisciplineId { get; set; }

        /// <summary>Оценка от 2 до 5</summary>
        [Range(2, 5)]
        public int Value { get; set; }

        /// <summary>Дата выставления оценки в формате ГГГГ-ММ-ДД</summary>
        public DateOnly Date { get; set; }
    }
}
