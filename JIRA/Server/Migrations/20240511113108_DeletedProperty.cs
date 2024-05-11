using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JIRA.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeletedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Attachments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dcab9951-4141-4bff-91ea-5deb52214fa0", "AQAAAAIAAYagAAAAEHfFO1NNTaDtSZ0qI1xHzO28u+AZb8z4cI94vHQknAA/2ZVf4cR8stOzd2bn7AUFrg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Attachments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f6ef84f-2f51-49f6-843d-5952f3cc504b", "AQAAAAIAAYagAAAAEJpYfENOqCbnUb6L8FcB63BmKsI/Ca3eiSQUKQuVHZdVR/2m/7Tut5/RKnpTxJDylg==" });
        }
    }
}
