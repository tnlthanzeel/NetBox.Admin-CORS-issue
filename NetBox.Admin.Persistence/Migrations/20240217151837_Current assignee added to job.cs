using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Currentassigneeaddedtojob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentAsigneeId",
                table: "Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Job_CurrentAsigneeId",
                table: "Job",
                column: "CurrentAsigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_CurrentAsigneeId",
                table: "Job",
                column: "CurrentAsigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_CurrentAsigneeId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_CurrentAsigneeId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CurrentAsigneeId",
                table: "Job");
        }
    }
}
