using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelMate.Migrations
{
    public partial class CovidDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoronaListCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewConfirmed = table.Column<int>(type: "int", nullable: false),
                    TotalConfirmed = table.Column<int>(type: "int", nullable: false),
                    NewDeaths = table.Column<int>(type: "int", nullable: false),
                    TotalDeaths = table.Column<int>(type: "int", nullable: false),
                    NewRecovered = table.Column<int>(type: "int", nullable: false),
                    TotalRecovered = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoronaListCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewConfirmed = table.Column<int>(type: "int", nullable: false),
                    TotalConfirmed = table.Column<int>(type: "int", nullable: false),
                    NewDeaths = table.Column<int>(type: "int", nullable: false),
                    TotalDeaths = table.Column<int>(type: "int", nullable: false),
                    NewRecovered = table.Column<int>(type: "int", nullable: false),
                    TotalRecovered = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalContexts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoronaListCountries_Country",
                table: "CoronaListCountries",
                column: "Country",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoronaListCountries");

            migrationBuilder.DropTable(
                name: "GlobalContexts");
        }
    }
}
