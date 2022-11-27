using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_RestricaoVideos.Migrations
{
    public partial class rest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restricoes",
                columns: table => new
                {
                    retricaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    restricaoNome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restricoes", x => x.retricaoId);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    videosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    videoDescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    videoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    restricaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.videosId);
                    table.ForeignKey(
                        name: "FK_Videos_Restricoes_restricaoId",
                        column: x => x.restricaoId,
                        principalTable: "Restricoes",
                        principalColumn: "retricaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_restricaoId",
                table: "Videos",
                column: "restricaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Restricoes");
        }
    }
}
