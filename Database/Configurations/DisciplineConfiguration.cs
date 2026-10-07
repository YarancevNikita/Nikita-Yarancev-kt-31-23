using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nikita_Yarancev_kt_31_23.Database.Helpers;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DisciplineId)
                .HasColumnName("discipline_id")
                .HasComment("Идентификатор записи дисциплины");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_discipline_name")
                .HasColumnType(ColumnType.String).HasMaxLength(200)
                .HasComment("Название дисциплины");

            builder.Property(p => p.Direction)
                .IsRequired()
                .HasColumnName("c_discipline_direction")
                .HasColumnType(ColumnType.Int)
                .HasComment("Направление дисциплины: 1 - гуманитарное, 2 - техническое");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("c_discipline_is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления записи");

            builder.ToTable(TableName);
        }
    }
}
