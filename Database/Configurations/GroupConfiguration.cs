using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nikita_Yarancev_kt_31_23.Database.Helpers;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.GroupId)
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(50)
                .HasComment("Название группы");

            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("c_group_course")
                .HasColumnType(ColumnType.Int)
                .HasComment("Курс обучения группы");

            builder.Property(p => p.Year)
                .IsRequired()
                .HasColumnName("c_group_year")
                .HasColumnType(ColumnType.Int)
                .HasComment("Год набора группы");

            builder.Property(p => p.SpecialtyId)
                .IsRequired()
                .HasColumnName("f_specialty_id")
                .HasComment("Идентификатор специальности");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("c_group_is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления записи");

            //Связь: у группы одна специальность, у специальности много групп
            builder.ToTable(TableName)
                .HasOne(p => p.Specialty)
                .WithMany()
                .HasForeignKey(p => p.SpecialtyId)
                .HasConstraintName("fk_f_specialty_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.SpecialtyId, $"idx_{TableName}_fk_f_specialty_id");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Specialty)
                .AutoInclude();
        }
    }
}
