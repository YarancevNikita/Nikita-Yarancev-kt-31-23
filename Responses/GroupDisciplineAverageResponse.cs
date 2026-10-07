namespace Nikita_Yarancev_kt_31_23.Responses
{
    //Средний балл по предмету в группе
    public class GroupDisciplineAverageResponse
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; } = string.Empty;

        //null, если оценок нет
        public double? AverageGrade { get; set; }

        public int GradesCount { get; set; }
    }
}
