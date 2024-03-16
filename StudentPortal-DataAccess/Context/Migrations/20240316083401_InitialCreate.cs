using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal_DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HumanResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserID = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserID = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassroomName = table.Column<string>(type: "text", nullable: true),
                    ClassroomDescription = table.Column<string>(type: "text", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserID = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Exam1 = table.Column<double>(type: "double precision", nullable: true),
                    Exam2 = table.Column<double>(type: "double precision", nullable: true),
                    ProjectExam = table.Column<double>(type: "double precision", nullable: true),
                    ProjectPath = table.Column<string>(type: "text", nullable: true),
                    ClassroomId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "HumanResources",
                columns: new[] { "Id", "AppUserID", "BirthDate", "CreatedDate", "DeletedDate", "Email", "FirstName", "HireDate", "LastName", "Status", "UpdatedDate" },
                values: new object[] { 1, "8b3cd4dd-84f7-4c44-8279-7124a458dfbf", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 11, 34, 0, 847, DateTimeKind.Local).AddTicks(3519), null, "hrpersonal@test.com", "İnsan Kaynakları", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "İnsan Kaynakları", 1, null });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AppUserID", "BirthDate", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "Status", "UpdatedDate" },
                values: new object[] { 1, "427f1691-2f27-44bb-b9f1-d1a4782381af", new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 11, 34, 0, 847, DateTimeKind.Local).AddTicks(2154), null, "teacher@test.com", "Öğretmen - 1", "Öğretmen - 1", 1, null });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "ClassroomDescription", "ClassroomName", "CreatedDate", "DeletedDate", "Status", "TeacherId", "UpdatedDate" },
                values: new object[] { 1, "320 Saat .NET Full Stack Yazılım Uzmanlığı Eğitimi", "YZL-8445", new DateTime(2024, 3, 16, 11, 34, 0, 847, DateTimeKind.Local).AddTicks(2784), null, 1, 1, null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AppUserID", "BirthDate", "ClassroomId", "CreatedDate", "DeletedDate", "Email", "Exam1", "Exam2", "FirstName", "LastName", "ProjectExam", "ProjectPath", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 3, 16, 11, 34, 0, 847, DateTimeKind.Local).AddTicks(3136), null, "student@test.com", null, null, "Öğrenci - 1", "Öğrenci - 1", null, null, 1, null },
                    { 2, "2735fcfe-c490-4055-ae67-18ae6eca2212", new DateTime(1996, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 3, 16, 11, 34, 0, 847, DateTimeKind.Local).AddTicks(3143), null, "student2@test.com", null, null, "Öğrenci - 2", "Öğrenci - 2", null, null, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumanResources");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
