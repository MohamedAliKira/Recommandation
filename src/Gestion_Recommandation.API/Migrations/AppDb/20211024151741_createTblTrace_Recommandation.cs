using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion_Recommandation.API.Migrations.AppDb
{
    public partial class createTblTrace_Recommandation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Recommandations",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Trace_Recommandations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Recommandations = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentSaisie = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DateSaisie = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trace_Recommandations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trace_Recommandations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Recommandations",
                newName: "ID");
        }
    }
}
