using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JIRA.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramChatId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TelegramChatId" },
                values: new object[] { "b16e179d-ceb0-4103-9a27-081c78fdd92a", "AQAAAAIAAYagAAAAEKD0ht11NFSyxv4AFG9Te0CccjcFzXYy+3j/3fH++0UWio/By5BKYAn0ZM0kZcubVg==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramChatId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dcab9951-4141-4bff-91ea-5deb52214fa0", "AQAAAAIAAYagAAAAEHfFO1NNTaDtSZ0qI1xHzO28u+AZb8z4cI94vHQknAA/2ZVf4cR8stOzd2bn7AUFrg==" });
        }
    }
}
