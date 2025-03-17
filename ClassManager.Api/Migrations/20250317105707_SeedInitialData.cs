using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "AccessCode", "Name", "Subject" },
                values: new object[,]
                {
                    { 1, "PHY123", "Physics Dept", "Physics" },
                    { 2, "HIS123", "History Dept", "History" },
                    { 3, "MTH123", "Maths Dept", "Maths" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsApproved", "Name", "PasswordHash", "Phone", "Role" },
                values: new object[,]
                {
                    { 1, "university@email.com", true, "University Admin", "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw==", "1234567890", "Admin" },
                    { 2, "manager@email.com", true, "University Manager", "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw==", "0987654321", "Manager" },
                    { 3, "department@email.com", true, "Department Head", "AQAAAAIAAYagAAAAEIC2CVjh/3b2Y5SBSYJfUJgEQX7iNUBUCFhLPY3/qYgHZWkwvU9kHL0O4aznN1YNUw==", "1122334455", "Department" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Quantum Mechanics" },
                    { 2, 1, "Thermodynamics" },
                    { 3, 2, "World History" },
                    { 4, 3, "Linear Algebra" },
                    { 5, 3, "Calculus" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
