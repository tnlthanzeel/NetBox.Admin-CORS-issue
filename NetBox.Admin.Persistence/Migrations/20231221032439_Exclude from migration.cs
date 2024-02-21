using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Excludefrommigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "PasswordHash",
                value: "AQAAAAIAB9AAAAAAEEBu3FWgRqgJWOiqjbX1yywHztopQIdUS0I23BsyaM8FxqlUionKPmAWRrD7obg/DQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJZHh/S5hmTm+8BR8ssy2GyMm04koddmCJLLGetMIWDEwKTXVwjow5mnIKwK5ExMNA==");
        }
    }
}
