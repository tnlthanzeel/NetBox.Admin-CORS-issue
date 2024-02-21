using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class indexcolumnsidechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_TokenNumberMasterDate_TokenNumber",
                table: "Job");

            migrationBuilder.CreateIndex(
                name: "IX_Job_TokenNumber_TokenNumberMasterDate",
                table: "Job",
                columns: new[] { "TokenNumber", "TokenNumberMasterDate" },
                unique: true,
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_TokenNumber_TokenNumberMasterDate",
                table: "Job");

            migrationBuilder.CreateIndex(
                name: "IX_Job_TokenNumberMasterDate_TokenNumber",
                table: "Job",
                columns: new[] { "TokenNumberMasterDate", "TokenNumber" },
                unique: true,
                descending: new[] { true, false });
        }
    }
}
