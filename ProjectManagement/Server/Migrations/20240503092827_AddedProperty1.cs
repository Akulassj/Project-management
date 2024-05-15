using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedProperty1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InActive",
                table: "TaskAssignees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f6ef84f-2f51-49f6-843d-5952f3cc504b", "AQAAAAIAAYagAAAAEJpYfENOqCbnUb6L8FcB63BmKsI/Ca3eiSQUKQuVHZdVR/2m/7Tut5/RKnpTxJDylg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InActive",
                table: "TaskAssignees");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "56bab0b9-0b2c-4990-a313-74186139a506", "AQAAAAIAAYagAAAAEMt2s6HnGwXRSBlQyvnSZmiEe8+F1bHqkLulnB7bdRkGoJ6Mex1+BhqxCo/G4Am5Rg==" });
        }
    }
}
