using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UniqueconstrainaddedforservicerelatedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Name",
                table: "ServiceType",
                column: "Name",
                unique: true,
                filter: "IsDeleted <> 1");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Name",
                table: "Service",
                column: "Name",
                unique: true,
                filter: "IsDeleted <> 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceType_Name",
                table: "ServiceType");

            migrationBuilder.DropIndex(
                name: "IX_Service_Name",
                table: "Service");
        }
    }
}
