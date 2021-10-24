using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion_Recommandation.API.Migrations.AppDb
{
    public partial class createTblRecommandation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recommandations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroReference = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    DateReference = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeLaPart = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IdentityRecommandation = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InstructionDRH = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_User = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommandations", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommandations");
        }
    }
}
