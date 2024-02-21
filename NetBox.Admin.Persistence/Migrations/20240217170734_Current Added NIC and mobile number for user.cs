using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CurrentAddedNICandmobilenumberforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "UserProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NICNumber",
                table: "UserProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);


            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "MobileNumber", "NICNumber" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "NICNumber",
                table: "UserProfile");
        }
    }
}
