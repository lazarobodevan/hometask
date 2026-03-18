using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hometask.Migrations
{
    /// <inheritdoc />
    public partial class minorFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById1",
                table: "HouseTasksCompletions");

            migrationBuilder.DropIndex(
                name: "IX_HouseTasksCompletions_CompletedById1",
                table: "HouseTasksCompletions");

            migrationBuilder.DropIndex(
                name: "IX_HouseTasksCompletions_HouseTaskId",
                table: "HouseTasksCompletions");

            migrationBuilder.DropColumn(
                name: "CompletedById1",
                table: "HouseTasksCompletions");

            migrationBuilder.AlterColumn<string>(
                name: "CompletedById",
                table: "HouseTasksCompletions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "WeekStart",
                table: "HouseTasksCompletions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_CompletedById",
                table: "HouseTasksCompletions",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_HouseTaskId_WeekStart",
                table: "HouseTasksCompletions",
                columns: new[] { "HouseTaskId", "WeekStart" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById",
                table: "HouseTasksCompletions",
                column: "CompletedById",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById",
                table: "HouseTasksCompletions");

            migrationBuilder.DropIndex(
                name: "IX_HouseTasksCompletions_CompletedById",
                table: "HouseTasksCompletions");

            migrationBuilder.DropIndex(
                name: "IX_HouseTasksCompletions_HouseTaskId_WeekStart",
                table: "HouseTasksCompletions");

            migrationBuilder.DropColumn(
                name: "WeekStart",
                table: "HouseTasksCompletions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompletedById",
                table: "HouseTasksCompletions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompletedById1",
                table: "HouseTasksCompletions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_CompletedById1",
                table: "HouseTasksCompletions",
                column: "CompletedById1");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_HouseTaskId",
                table: "HouseTasksCompletions",
                column: "HouseTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById1",
                table: "HouseTasksCompletions",
                column: "CompletedById1",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
