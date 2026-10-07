namespace Nikita_Yarancev_kt_31_23.Requests
{
    //Данные для добавления/изменения зачета
    public class CreditRequest
    {
        public int StudentId { get; set; }

        public int DisciplineId { get; set; }

        /// <summary>true - зачтено, false - не зачтено</summary>
        public bool IsPassed { get; set; }

        /// <summary>Дата сдачи зачета в формате ГГГГ-ММ-ДД</summary>
        public DateOnly Date { get; set; }
    }
}
