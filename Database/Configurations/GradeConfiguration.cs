using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nikita_Yarancev_kt_31_23.Database.Helpers;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.GradeId)
                .HasColumnName("grade_id")
                .HasComment("Идентификатор записи оценки");

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("c_grade_value")
                .HasColumnType(ColumnType.Int)
                .HasComment("Значение оценки");

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("f_student_id")
                .HasComment("Идентификатор студента");

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("f_discipline_id")
                .HasComment("Идентификатор дисциплины");

            //Связь: у оценки один студент, у студента много оценок
            builder.ToTable(TableName)
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            //Связь: у оценки одна дисциплина, у дисциплины много оценок
            builder.ToTable(TableName)
                .HasOne(p => p.Discipline)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");

            builder.ToTable(TableName)
                .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            //Добавим явную автоподгрузку связанных сущностей
            builder.Navigation(p => p.Student)
                .AutoInclude();

            builder.Navigation(p => p.Discipline)
                .AutoInclude();
        }
    }
}
