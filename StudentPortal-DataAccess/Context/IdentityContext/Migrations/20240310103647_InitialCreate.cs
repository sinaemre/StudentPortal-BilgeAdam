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
                    { "2735fcfe-c490-4055-ae67-18ae6eca2212", 0, new DateTime(1996, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "a3dbdbd8-b4db-451c-b469-d724872f087d", new DateTime(2024, 3, 10, 13, 36, 45, 724, DateTimeKind.Local).AddTicks(1606), null, "student2@test.com", false, "Öğrenci - 2", "Öğrenci - 2", false, null, 0, "STUDENT2@TEST.COM", "STUDENT2", "AQAAAAIAAYagAAAAEMBvklSyskrguEMpGG04CCP/fm2cOVm1Xxrx7ElZ91offmYQaJq/2LmjZx6a4hYLJQ==", null, false, "2f524384-55a6-4777-9937-168d7902f571", 1, false, null, "student2" },
                    { "427f1691-2f27-44bb-b9f1-d1a4782381af", 0, new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbc7a72a-5761-4ac8-8efd-bc299dce6efa", new DateTime(2024, 3, 10, 13, 36, 45, 812, DateTimeKind.Local).AddTicks(4055), null, "teacher@test.com", false, "Öğretmen - 1", "Öğretmen - 1", false, null, 0, "TEACHER@TEST.COM", "TEACHER", "AQAAAAIAAYagAAAAEKX1gLWpVii8WkuNumgSZqjDEzjL6ske7fvt+Qi8/yd09lAhybsIl/D43iNmiPfwew==", null, false, "8c72cbb3-9c6b-4ffd-a42d-d46aa8308a47", 1, false, null, "teacher" },
                    { "8b3cd4dd-84f7-4c44-8279-7124a458dfbf", 0, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0195da5c-c9d5-4226-95b3-d4cf9aa80d20", new DateTime(2024, 3, 10, 13, 36, 45, 900, DateTimeKind.Local).AddTicks(5825), null, "hrpersonal@test.com", false, "İnsan Kaynakları", "İnsan Kaynakları", false, null, 0, "HRPERSONAL@TEST.COM", "HRPERSONAL", "AQAAAAIAAYagAAAAEADO7ECJ0+AbvEgDhTBq2Fz5A1qggBOcjSf1j2+1OJO2D2A0D6BUjsF5h6eSOBwHCg==", null, false, "51599837-eefa-4132-a952-9811dc1467ad", 1, false, null, "hrPersonal" },
                    { "92881b6d-cb5d-4809-b964-91074a5184d1", 0, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4db3fac8-d6f1-4696-8ab7-d332bc887020", new DateTime(2024, 3, 10, 13, 36, 45, 546, DateTimeKind.Local).AddTicks(4156), null, "admin@test.com", false, "Admin", "Admin", false, null, 0, "ADMIN@TEST.COM", "ADMIN", "AQAAAAIAAYagAAAAEH7MDvQNOAnZgtrGula++EFhBH9Wcj3rZvMxVLBQHUQAJeia6wRtb+CWzRY4qYFCVw==", null, false, "e6b25fea-3992-428b-9bef-1f373313146e", 1, false, null, "admin" },
                    { "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1", 0, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5fa29671-55b6-40f0-9a5a-80eb6c3c9d9b", new DateTime(2024, 3, 10, 13, 36, 45, 633, DateTimeKind.Local).AddTicks(4820), null, "student@test.com", false, "Öğrenci - 1", "Öğrenci - 1", false, null, 0, "STUDENT@TEST.COM", "STUDENT", "AQAAAAIAAYagAAAAEAoUR5GyoumrYMZ6dbGoAMXjHBBp5cqRVafbaA+YDnlc5cC0MMUhJlgwM9UiNVAeCg==", null, false, "986b6a37-83b6-4583-84a7-23c7c5d5f195", 1, false, null, "student" }
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
