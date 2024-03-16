using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal_DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHumanResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 14, 8, 32, 274, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.InsertData(
                table: "HumanResources",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "DeletedDate", "Email", "FirstName", "HireDate", "LastName", "Status", "UpdatedDate" },
                values: new object[] { 1, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 14, 8, 32, 274, DateTimeKind.Local).AddTicks(5973), null, "hrpersonal@test.com", "İnsan Kaynakları", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "İnsan Kaynakları", 1, null });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 14, 8, 32, 274, DateTimeKind.Local).AddTicks(5642));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 14, 8, 32, 274, DateTimeKind.Local).AddTicks(5649));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 14, 8, 32, 274, DateTimeKind.Local).AddTicks(4813));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HumanResources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 13, 37, 19, 285, DateTimeKind.Local).AddTicks(2804));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 13, 37, 19, 285, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 13, 37, 19, 285, DateTimeKind.Local).AddTicks(3172));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 10, 13, 37, 19, 285, DateTimeKind.Local).AddTicks(2291));
        }
    }
}
