using Microsoft.EntityFrameworkCore;
using Nikita_Yarancev_kt_31_23.Database.Configurations;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database
{
    public class StudentDbContext : DbContext
    {
        //Добавляем таблицы
        public DbSet<Specialty> Specialtys { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Credit> Credits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new CreditConfiguration());
        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
    }
}
