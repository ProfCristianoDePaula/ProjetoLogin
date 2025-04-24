using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetoLogin.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00bb3b24-e535-4216-99ea-1efd9333e650", null, "Coordenador", "COORDENADOR" },
                    { "8d552d7b-0eda-44ae-b5ea-3d32a34a8656", null, "Psicólogo", "PSICÓLOGO" },
                    { "ccd536b3-3289-40f8-a002-38f9557d1b60", null, "Operador", "OPERADOR" },
                    { "df66d997-8e6d-4758-abe9-4bd2bfe31c0d", null, "Admin", "ADMIN" },
                    { "fbe16643-7088-436e-8dd4-20112e9f5d12", null, "Professor", "PROFESSOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "17fb5680-07d9-4bb9-9863-dc71415dceb3", 0, "d21ffb34-0e1b-4307-a320-a35e0acd105a", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEKbqQfhvsCFCy1FfERCv29sTlnIMW3Nl15GfzcnNigyVvK5ygKGLpCv7piVcZtW3fA==", null, false, "67d6bae8-129e-4927-89d3-3cd82d519608", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "df66d997-8e6d-4758-abe9-4bd2bfe31c0d", "17fb5680-07d9-4bb9-9863-dc71415dceb3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00bb3b24-e535-4216-99ea-1efd9333e650");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d552d7b-0eda-44ae-b5ea-3d32a34a8656");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccd536b3-3289-40f8-a002-38f9557d1b60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbe16643-7088-436e-8dd4-20112e9f5d12");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "df66d997-8e6d-4758-abe9-4bd2bfe31c0d", "17fb5680-07d9-4bb9-9863-dc71415dceb3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df66d997-8e6d-4758-abe9-4bd2bfe31c0d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17fb5680-07d9-4bb9-9863-dc71415dceb3");
        }
    }
}
