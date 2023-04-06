using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RatingEnumAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Rating",
                table: "Movies",
                type: "tinyint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("165bbd6d-d2de-434b-a3fb-908b8606df64"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("1ea14dbc-996f-4bea-815d-c92850ec8645"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("b357da7d-5141-4e71-9d73-9c642c081be0"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("e1229a0e-b210-4151-957a-34f9e5ba299a"),
                column: "Rating",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");
        }
    }
}
