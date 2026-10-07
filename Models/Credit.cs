namespace Nikita_Yarancev_kt_31_23.Models
{
    public class Credit
    {
        public int CreditId { get; set; }

        public bool IsPassed { get; set; }

        public DateOnly Date { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }
    }
}
