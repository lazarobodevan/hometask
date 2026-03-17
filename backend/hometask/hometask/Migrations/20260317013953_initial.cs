using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hometask.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "HouseTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    HouseAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    RotationIndex = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseTasks_HouseAreas_HouseAreaId",
                        column: x => x.HouseAreaId,
                        principalTable: "HouseAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseTasksCompletions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<string>(type: "text", nullable: false),
                    CompletedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CompletedByName = table.Column<string>(type: "text", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseTasksCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseTasksCompletions_HouseTasks_HouseTaskId",
                        column: x => x.HouseTaskId,
                        principalTable: "HouseTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseTasksCompletions_People_CompletedByName",
                        column: x => x.CompletedByName,
                        principalTable: "People",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_HouseTasksCompletions_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseTasksParticipants",
                columns: table => new
                {
                    HouseTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<string>(type: "text", nullable: false),
                    RotationOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseTasksParticipants", x => new { x.HouseTaskId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_HouseTasksParticipants_HouseTasks_HouseTaskId",
                        column: x => x.HouseTaskId,
                        principalTable: "HouseTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseTasksParticipants_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasks_HouseAreaId",
                table: "HouseTasks",
                column: "HouseAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_CompletedByName",
                table: "HouseTasksCompletions",
                column: "CompletedByName");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_HouseTaskId",
                table: "HouseTasksCompletions",
                column: "HouseTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksCompletions_PersonId",
                table: "HouseTasksCompletions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTasksParticipants_PersonId",
                table: "HouseTasksParticipants",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseTasksCompletions");

            migrationBuilder.DropTable(
                name: "HouseTasksParticipants");

            migrationBuilder.DropTable(
                name: "HouseTasks");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "HouseAreas");
        }
    }
}
