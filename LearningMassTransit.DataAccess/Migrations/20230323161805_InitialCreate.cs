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

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                schema: "lara",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "VoorstellenAdresState",
                schema: "lara");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "lara");
        }
    }
}
