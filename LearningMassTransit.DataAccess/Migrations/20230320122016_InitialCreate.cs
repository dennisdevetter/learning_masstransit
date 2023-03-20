using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "Blogs",
                schema: "lara",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Wizards",
                schema: "lara",
                columns: table => new
                {
                    WizardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizards", x => x.WizardId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "lara",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    BlogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "lara",
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WizardSteps",
                schema: "lara",
                columns: table => new
                {
                    WizardStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    StepNr = table.Column<int>(type: "integer", nullable: false),
                    StepData = table.Column<string>(type: "text", nullable: false),
                    StepType = table.Column<string>(type: "text", nullable: false),
                    WizardId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketId = table.Column<string>(type: "text", nullable: true),
                    TicketData = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizardSteps", x => x.WizardStepId);
                    table.ForeignKey(
                        name: "FK_WizardSteps_Wizards_WizardId",
                        column: x => x.WizardId,
                        principalSchema: "lara",
                        principalTable: "Wizards",
                        principalColumn: "WizardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                schema: "lara",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_WizardSteps_WizardId",
                schema: "lara",
                table: "WizardSteps",
                column: "WizardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "WizardSteps",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Wizards",
                schema: "lara");
        }
    }
}
