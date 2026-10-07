using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nikita_Yarancev_kt_31_23.Database.Helpers;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database.Configurations
{
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_credit";

        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.CreditId)
                .HasName($"pk_{TableName}_credit_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            builder.Property(p => p.CreditId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CreditId)
                .HasColumnName("credit_id")
                .HasComment("Идентификатор записи зачета");

            builder.Property(p => p.IsPassed)
                .IsRequired()
                .HasColumnName("c_credit_is_passed")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Признак сдачи зачета");

            builder.Property(p => p.Date)
                .IsRequired()
                .HasColumnName("c_credit_date")
                .HasColumnType(ColumnType.DateOnly)
                .HasComment("Дата сдачи зачета");

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("f_student_id")
                .HasComment("Идентификатор студента");

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("f_discipline_id")
                .HasComment("Идентификатор дисциплины");

            //Связь: у зачета один студент, у студента много зачетов
            builder.ToTable(TableName)
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_cd_credit_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            //Связь: у зачета одна дисциплина, у дисциплины много зачетов
            builder.ToTable(TableName)
                .HasOne(p => p.Discipline)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_cd_credit_f_discipline_id")
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
