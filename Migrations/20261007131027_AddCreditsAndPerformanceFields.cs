using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nikita_Yarancev_kt_31_23.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditsAndPerformanceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "c_student_middlename",
                table: "cd_student",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                comment: "Отчество студента");

            migrationBuilder.AddColumn<int>(
                name: "c_group_year",
                table: "cd_group",
                type: "int4",
                nullable: false,
                defaultValue: 0,
                comment: "Год набора группы");

            migrationBuilder.AddColumn<DateOnly>(
                name: "c_grade_date",
                table: "cd_grade",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                comment: "Дата выставления оценки");

            migrationBuilder.AddColumn<int>(
                name: "c_discipline_direction",
                table: "cd_discipline",
                type: "int4",
                nullable: false,
                defaultValue: 2,
                comment: "Направление дисциплины: 1 - гуманитарное, 2 - техническое");

            migrationBuilder.CreateTable(
                name: "cd_credit",
                columns: table => new
                {
                    credit_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи зачета")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_credit_is_passed = table.Column<bool>(type: "bool", nullable: false, comment: "Признак сдачи зачета"),
                    c_credit_date = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата сдачи зачета"),
                    f_student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор студента"),
                    f_discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_credit_credit_id", x => x.credit_id);
                    table.ForeignKey(
                        name: "fk_cd_credit_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cd_credit_f_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_fk_f_discipline_id",
                table: "cd_credit",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_fk_f_student_id",
                table: "cd_credit",
                column: "f_student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_credit");

            migrationBuilder.DropColumn(
                name: "c_student_middlename",
                table: "cd_student");

            migrationBuilder.DropColumn(
                name: "c_group_year",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "c_grade_date",
                table: "cd_grade");

            migrationBuilder.DropColumn(
                name: "c_discipline_direction",
                table: "cd_discipline");
        }
    }
}
