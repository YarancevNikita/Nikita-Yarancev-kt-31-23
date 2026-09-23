using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nikita_Yarancev_kt_31_23.Database.Helpers;
using Nikita_Yarancev_kt_31_23.Models;

namespace Nikita_Yarancev_kt_31_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_specialty";

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.SpecialtyId)
                .HasName($"pk_{TableName}_specialty_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            builder.Property(p => p.SpecialtyId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.SpecialtyId)
                .HasColumnName("specialty_id")
                .HasComment("Идентификатор записи специальности");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("c_specialty_title")
                .HasColumnType(ColumnType.String).HasMaxLength(200)
                .HasComment("Название специальности");

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnName("c_specialty_code")
                .HasColumnType(ColumnType.String).HasMaxLength(20)
                .HasComment("Код специальности");

            builder.ToTable(TableName);
        }
    }
}
