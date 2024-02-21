using Microsoft.EntityFrameworkCore.Migrations;
using NetBox.Admin.Core.Jobs;
using static NetBox.Admin.SharedKernal.AppEnums;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Jobasigneetabledesignerjobprogress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesignerJobProgress",
                table: "DesignerAssignedJob",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: nameof(DesignerJobProgress.NotStarted));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignerJobProgress",
                table: "DesignerAssignedJob");
        }
    }
}
