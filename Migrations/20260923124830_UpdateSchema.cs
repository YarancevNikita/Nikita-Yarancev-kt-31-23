using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nikita_Yarancev_kt_31_23.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_student_middlename",
                table: "cd_student");

            migrationBuilder.AddColumn<bool>(
                name: "c_student_is_deleted",
                table: "cd_student",
                type: "bool",
                nullable: false,
                defaultValue: false,
                comment: "Признак удаления записи");

            migrationBuilder.AddColumn<int>(
                name: "c_group_course",
                table: "cd_group",
                type: "int4",
                nullable: false,
                defaultValue: 0,
                comment: "Курс обучения группы");

            migrationBuilder.AddColumn<bool>(
                name: "c_group_is_deleted",
                table: "cd_group",
                type: "bool",
                nullable: false,
                defaultValue: false,
                comment: "Признак удаления записи");

            migrationBuilder.AddColumn<int>(
                name: "f_specialty_id",
                table: "cd_group",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Идентификатор специальности");

            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название дисциплины"),
                    c_discipline_is_deleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false, comment: "Признак удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_specialty",
                columns: table => new
                {
                    specialty_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи специальности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_specialty_title = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название специальности"),
                    c_specialty_code = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialty_specialty_id", x => x.specialty_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи оценки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "int4", nullable: false, comment: "Значение оценки"),
                    f_student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор студента"),
                    f_discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.grade_id);
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_fk_f_specialty_id",
                table: "cd_group",
                column: "f_specialty_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_discipline_id",
                table: "cd_grade",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_student_id",
                table: "cd_grade",
                column: "f_student_id");

            migrationBuilder.AddForeignKey(
                name: "fk_f_specialty_id",
                table: "cd_group",
                column: "f_specialty_id",
                principalTable: "cd_specialty",
                principalColumn: "specialty_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_f_specialty_id",
                table: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_specialty");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropIndex(
                name: "idx_cd_group_fk_f_specialty_id",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "c_student_is_deleted",
                table: "cd_student");

            migrationBuilder.DropColumn(
                name: "c_group_course",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "c_group_is_deleted",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "f_specialty_id",
                table: "cd_group");

            migrationBuilder.AddColumn<string>(
                name: "c_student_middlename",
                table: "cd_student",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Отчество студента");
        }
    }
}
