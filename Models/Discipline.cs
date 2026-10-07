namespace Nikita_Yarancev_kt_31_23.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }

        public string Name { get; set; }

        public DisciplineDirection Direction { get; set; }

        public bool IsDeleted { get; set; }
    }
}
