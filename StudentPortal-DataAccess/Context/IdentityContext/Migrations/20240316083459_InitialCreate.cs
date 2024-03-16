using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal_DataAccess.Context.IdentityContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LoginCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "74f37192-b74b-4330-b875-372e82c04002", null, "student", "STUDENT" },
                    { "b609c887-e794-4762-be5a-6c95232812a4", null, "admin", "ADMIN" },
                    { "dd8eadf8-f90f-41f3-9d81-096ef9e7829b", null, "hrPersonal", "HRPERSONAL" },
                    { "e256341e-70f6-4573-b09a-ab4205a7efc6", null, "teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LoginCount", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { "2735fcfe-c490-4055-ae67-18ae6eca2212", 0, new DateTime(1996, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "3f4fb6ff-c383-464d-8630-e4f546d28f17", new DateTime(2024, 3, 16, 11, 34, 58, 786, DateTimeKind.Local).AddTicks(8699), null, "student2@test.com", false, "Öğrenci - 2", "Öğrenci - 2", false, null, 0, "STUDENT2@TEST.COM", "STUDENT2", "AQAAAAIAAYagAAAAEFJY8DfdUE5x24ye4iaNdmW17Yijmm5Ofv1hYvaAmpmhgHnFPTs4DiRXOjswfRyMTg==", null, false, "94a72359-5ca7-40c3-ac89-68c8d4d69866", 1, false, null, "student2" },
                    { "427f1691-2f27-44bb-b9f1-d1a4782381af", 0, new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "bbecf63d-2c7a-4cc5-ae9e-de5e8929ca79", new DateTime(2024, 3, 16, 11, 34, 58, 872, DateTimeKind.Local).AddTicks(1163), null, "teacher@test.com", false, "Öğretmen - 1", "Öğretmen - 1", false, null, 0, "TEACHER@TEST.COM", "TEACHER", "AQAAAAIAAYagAAAAEPWfDy9pFNEDlINpAA4xb5bf9oTxFI8zqpxFoK5lJVn+13cBYaNj/HZgX4PKkx0QEg==", null, false, "1b2b2a04-f547-4707-b3d0-c04f55c0da93", 1, false, null, "teacher" },
                    { "8b3cd4dd-84f7-4c44-8279-7124a458dfbf", 0, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bc6344e7-1513-4823-958e-d1bdaddb0738", new DateTime(2024, 3, 16, 11, 34, 58, 957, DateTimeKind.Local).AddTicks(5845), null, "hrpersonal@test.com", false, "İnsan Kaynakları", "İnsan Kaynakları", false, null, 0, "HRPERSONAL@TEST.COM", "HRPERSONAL", "AQAAAAIAAYagAAAAEGP/2cWMwjJW5n7UWSIwVsJaaHTB1v4M/PFwlQ5KLKkafAUwry4fXBENhIuwq+K70Q==", null, false, "54ac2e10-b78a-4a1f-82cc-98122d610378", 1, false, null, "hrPersonal" },
                    { "92881b6d-cb5d-4809-b964-91074a5184d1", 0, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4932e8fc-7ef7-4287-ac35-a92fbdd7db94", new DateTime(2024, 3, 16, 11, 34, 58, 615, DateTimeKind.Local).AddTicks(8530), null, "admin@test.com", false, "Admin", "Admin", false, null, 0, "ADMIN@TEST.COM", "ADMIN", "AQAAAAIAAYagAAAAEEzh42p+hljhFrdsxTfZQSJy8/eZ+Y3Dc0rF4SeWOJFZM/C7arL8/ztt0ht887ySdw==", null, false, "910ff034-0620-403f-a4f5-d5d966bb9020", 1, false, null, "admin" },
                    { "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1", 0, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a255e396-5be8-4e16-a21b-a10acbba709a", new DateTime(2024, 3, 16, 11, 34, 58, 701, DateTimeKind.Local).AddTicks(2193), null, "student@test.com", false, "Öğrenci - 1", "Öğrenci - 1", false, null, 0, "STUDENT@TEST.COM", "STUDENT", "AQAAAAIAAYagAAAAEMtAsKhKzlnbd6wZB7qg2h9Z42htUsmGbjviatQnsqA9pLspZqZdOvciB9Ai40ChQQ==", null, false, "c51e805f-ca4a-40f1-add3-39e50b307615", 1, false, null, "student" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "74f37192-b74b-4330-b875-372e82c04002", "2735fcfe-c490-4055-ae67-18ae6eca2212" },
                    { "e256341e-70f6-4573-b09a-ab4205a7efc6", "427f1691-2f27-44bb-b9f1-d1a4782381af" },
                    { "dd8eadf8-f90f-41f3-9d81-096ef9e7829b", "8b3cd4dd-84f7-4c44-8279-7124a458dfbf" },
                    { "b609c887-e794-4762-be5a-6c95232812a4", "92881b6d-cb5d-4809-b964-91074a5184d1" },
                    { "74f37192-b74b-4330-b875-372e82c04002", "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
