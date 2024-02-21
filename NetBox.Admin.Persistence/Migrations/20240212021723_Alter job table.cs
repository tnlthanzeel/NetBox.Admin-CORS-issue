using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBox.Admin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alterjobtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientTypeId",
                table: "Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "JobTypeId",
                table: "Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PhonNumber",
                table: "Job",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TokenNumber",
                table: "Job",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ClientTypeId",
                table: "Job",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTypeId",
                table: "Job",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_ClientType_ClientTypeId",
                table: "Job",
                column: "ClientTypeId",
                principalTable: "ClientType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobType_JobTypeId",
                table: "Job",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_ClientType_ClientTypeId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobType_JobTypeId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_ClientTypeId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobTypeId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PhonNumber",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "TokenNumber",
                table: "Job");
        }
    }
}
