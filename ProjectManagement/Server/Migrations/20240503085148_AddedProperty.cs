using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "56bab0b9-0b2c-4990-a313-74186139a506", "AQAAAAIAAYagAAAAEMt2s6HnGwXRSBlQyvnSZmiEe8+F1bHqkLulnB7bdRkGoJ6Mex1+BhqxCo/G4Am5Rg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46e877c0-335b-4a8a-9019-5ce2b8f4e9a8", "AQAAAAIAAYagAAAAEHyej2i7xGMrUaH9ID3zrBZJRvPrBN6jSkUUbN9sZ5c55MPIVfqPtuW1e2MTZOPijQ==" });
        }
    }
}
