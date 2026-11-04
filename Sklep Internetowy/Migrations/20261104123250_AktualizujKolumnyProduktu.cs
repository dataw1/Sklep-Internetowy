using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class AktualizujKolumnyProduktu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyAktywny",
                table: "Uzytkownicy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Nazwa",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "KategoriaIdKategorii",
                table: "Produkty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    IdKategorii = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.IdKategorii);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZlozenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LacznyKoszt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdresDostawyUlica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresDostawyKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresDostawyMiasto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetodaPlatnosci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetodaDostawy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KlientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Uzytkownicy_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "IdUzytkownik");
                });

            migrationBuilder.UpdateData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 1,
                column: "KategoriaIdKategorii",
                value: null);

            migrationBuilder.UpdateData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 2,
                column: "KategoriaIdKategorii",
                value: null);

            migrationBuilder.UpdateData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 3,
                column: "KategoriaIdKategorii",
                value: null);

            migrationBuilder.UpdateData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 4,
                column: "KategoriaIdKategorii",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_KategoriaIdKategorii",
                table: "Produkty",
                column: "KategoriaIdKategorii");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_KlientId",
                table: "Zamowienia",
                column: "KlientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkty_Kategorie_KategoriaIdKategorii",
                table: "Produkty",
                column: "KategoriaIdKategorii",
                principalTable: "Kategorie",
                principalColumn: "IdKategorii");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkty_Kategorie_KategoriaIdKategorii",
                table: "Produkty");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Zamowienia");

            migrationBuilder.DropIndex(
                name: "IX_Produkty_KategoriaIdKategorii",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "CzyAktywny",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "KategoriaIdKategorii",
                table: "Produkty");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Produkty",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nazwa",
                table: "Produkty",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
