using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Changedtimecolimtocumulatedminutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "JobTimer");

            migrationBuilder.AddColumn<int>(
                name: "CumulatedMinutes",
                table: "JobTimer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CumulatedMinutes",
                table: "JobTimer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "JobTimer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
