using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jobstatusenumadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobStatus",
                table: "Job",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "Job");
        }
    }
}
