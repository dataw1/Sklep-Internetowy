using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class DodanieProduktowISeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    IdProdukt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZdjecieUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IloscWMagazynie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.IdProdukt);
                });

            migrationBuilder.InsertData(
                table: "Produkty",
                columns: new[] { "IdProdukt", "Cena", "IloscWMagazynie", "Kategoria", "Nazwa", "Opis", "ZdjecieUrl" },
                values: new object[,]
                {
                    { 1, 299.99m, 15, "Akcesoria", "Klawiatura mechaniczna RGB", "Przełączniki Red, cicha praca, podświetlenie RGB, układ PL.", "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=500&auto=format&fit=crop&q=60" },
                    { 2, 149.50m, 25, "Akcesoria", "Mysz bezprzewodowa Pro", "Sensor 16000 DPI, ergonomiczny kształt, do 70h na jednym ładowaniu.", "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500&auto=format&fit=crop&q=60" },
                    { 3, 219.00m, 8, "Audio", "Słuchawki Gamingowe 7.1", "Dźwięk przestrzenny, mikrofon z redukcją szumów, miękkie nauszniki.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500&auto=format&fit=crop&q=60" },
                    { 4, 1099.00m, 5, "Monitory", "Monitor Gamingowy 27'' IPS", "Rozdzielczość 1440p, odświeżanie 165Hz, czas reakcji 1ms.", "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500&auto=format&fit=crop&q=60" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produkty");
        }
    }
}
