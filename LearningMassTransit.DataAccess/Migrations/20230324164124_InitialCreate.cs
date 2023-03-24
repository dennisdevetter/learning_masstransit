using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningMassTransit.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lara");

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "lara",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Actie = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "VoorstellenAdresState",
                schema: "lara",
                columns: table => new
                {
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoorstellenAdresState", x => x.WorkflowId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "VoorstellenAdresState",
                schema: "lara");
        }
    }
}
