namespace Nikita_Yarancev_kt_31_23.Responses
{
    //Средний балл за год
    public class YearAverageResponse
    {
        public int Year { get; set; }

        public int? GroupId { get; set; }

        public int? DisciplineId { get; set; }

        //null, если оценок нет
        public double? AverageGrade { get; set; }

        public int GradesCount { get; set; }
    }
}
