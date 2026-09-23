namespace Nikita_Yarancev_kt_31_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public bool IsDeleted { get; set; }
    }
}
