using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutBoxEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSuccesful = table.Column<bool>(type: "bit", nullable: false),
                    TryCount = table.Column<int>(type: "int", nullable: false),
                    TodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutBoxEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutBoxEmails_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutBoxEmails_TodoId",
                table: "OutBoxEmails",
                column: "TodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutBoxEmails");
        }
    }
}
