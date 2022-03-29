using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion_Recommandation.API.Migrations.AppDb
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recommandations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReference = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeLaPart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityRecommandation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructionDRH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bureau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_User = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommandations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trace_Recommandations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Recommandations = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentBureau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentSaisie = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Recommandations");

            migrationBuilder.DropTable(
                name: "Trace_Recommandations");
        }
    }
}
