using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hometask.Migrations
{
    /// <inheritdoc />
    public partial class addedPersonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedByName",
                table: "HouseTasksCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_PersonId",
                table: "HouseTasksCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksParticipants_People_PersonId",
                table: "HouseTasksParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "CompletedByName",
                table: "HouseTasksCompletions",
                newName: "CompletedById1");

            migrationBuilder.RenameIndex(
                name: "IX_HouseTasksCompletions_CompletedByName",
                table: "HouseTasksCompletions",
                newName: "IX_HouseTasksCompletions_CompletedById1");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "People",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById1",
                table: "HouseTasksCompletions",
                column: "CompletedById1",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_PersonId",
                table: "HouseTasksCompletions",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksParticipants_People_PersonId",
                table: "HouseTasksParticipants",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedById1",
                table: "HouseTasksCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksCompletions_People_PersonId",
                table: "HouseTasksCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseTasksParticipants_People_PersonId",
                table: "HouseTasksParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "CompletedById1",
                table: "HouseTasksCompletions",
                newName: "CompletedByName");

            migrationBuilder.RenameIndex(
                name: "IX_HouseTasksCompletions_CompletedById1",
                table: "HouseTasksCompletions",
                newName: "IX_HouseTasksCompletions_CompletedByName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_CompletedByName",
                table: "HouseTasksCompletions",
                column: "CompletedByName",
                principalTable: "People",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksCompletions_People_PersonId",
                table: "HouseTasksCompletions",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseTasksParticipants_People_PersonId",
                table: "HouseTasksParticipants",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
