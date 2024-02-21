using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alteredserviceandcustomertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Service",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Name",
                table: "Customer",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PhoneNumber",
                table: "Customer",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_Name",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_PhoneNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Service");
        }
    }
}
