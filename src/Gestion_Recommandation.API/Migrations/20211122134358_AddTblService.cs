using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestion_Recommandation.API.Migrations
{
    public partial class AddTblService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuttelle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Libelle", "Tuttelle" },
                values: new object[,]
                {
                    { 1, "DRH", 1 },
                    { 22, "BSF", 4 },
                    { 21, "BRAA", 4 },
                    { 20, "BS", 4 },
                    { 19, "BAJ", 3 },
                    { 18, "BRP", 3 },
                    { 17, "BCM", 3 },
                    { 16, "BAT", 3 },
                    { 15, "BPM", 2 },
                    { 14, "BDA", 2 },
                    { 13, "BGC", 2 },
                    { 12, "BESM", 2 },
                    { 11, "BCAA", 1 },
                    { 10, "BPF", 1 },
                    { 9, "BFA", 1 },
                    { 8, "BAG", 1 },
                    { 7, "BI", 1 },
                    { 6, "BIC", 1 },
                    { 5, "SCPN", 1 },
                    { 4, "SDRS", 1 },
                    { 3, "SDC", 1 },
                    { 2, "SDP", 1 },
                    { 23, "BEP", 4 },
                    { 24, "BPSY", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
