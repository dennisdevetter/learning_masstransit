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
                name: "Workflow",
                schema: "lara",
                columns: table => new
                {
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkflowAction = table.Column<int>(type: "integer", nullable: false),
                    WorkflowType = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.WorkflowId);
                });

            migrationBuilder.CreateTable(
                name: "AtomaireActieState",
                schema: "lara",
                columns: table => new
                {
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    Actie = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomaireActieState", x => x.WorkflowId);
                    table.ForeignKey(
                        name: "FK_AtomaireActieState_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "lara",
                        principalTable: "Workflow",
                        principalColumn: "WorkflowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoorstellenAdresState",
                schema: "lara",
                columns: table => new
                {
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoorstellenAdresState", x => x.WorkflowId);
                    table.ForeignKey(
                        name: "FK_VoorstellenAdresState_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "lara",
                        principalTable: "Workflow",
                        principalColumn: "WorkflowId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtomaireActieState",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "VoorstellenAdresState",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Workflow",
                schema: "lara");
        }
    }
}
