using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alterjobtableuniquefeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "TokenNumberMasterDate",
                table: "Job",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Job_TokenNumber",
                table: "Job",
                column: "TokenNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Job_TokenNumberMasterDate_TokenNumber",
                table: "Job",
                columns: new[] { "TokenNumberMasterDate", "TokenNumber" },
                unique: true,
                descending: new[] { true, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_TokenNumber",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_TokenNumberMasterDate_TokenNumber",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "TokenNumberMasterDate",
                table: "Job");
        }
    }
}
