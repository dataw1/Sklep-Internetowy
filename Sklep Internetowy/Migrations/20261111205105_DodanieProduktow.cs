using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sklep_Internetowy.Migrations
{
    /// <inheritdoc />
    public partial class DodanieProduktow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produkty",
                columns: new[] { "IdProdukt", "Cena", "IloscWMagazynie", "Kategoria", "KategoriaIdKategorii", "Nazwa", "Opis", "ZdjecieUrl" },
                values: new object[,]
                {
                    { 5, 59.99m, 40, "Akcesoria", null, "Podkładka pod mysz XXL", "Wymiary 900x400 mm, obszyte krawędzie, antypoślizgowa guma.", "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500&auto=format&fit=crop&q=60" },
                    { 6, 349.00m, 18, "Peryferia", null, "Kamerka internetowa 4K Pro", "Autofocus, wbudowany mikrofon stereo, osłona obiektywu.", "https://images.unsplash.com/photo-1588508065123-287b28e013da?w=500&auto=format&fit=crop&q=60" },
                    { 7, 279.90m, 12, "Audio", null, "Mikrofon pojemnościowy USB", "Studyjna jakość nagrywania, koszyk antywstrząsowy i pop-filtr.", "https://images.unsplash.com/photo-1590602847861-f357a9332bbc?w=500&auto=format&fit=crop&q=60" },
                    { 8, 389.00m, 30, "Dyski i pamięci", null, "Dysk SSD NVMe 1TB PCIe 4.0", "Odczyt do 7000 MB/s, zapis do 5000 MB/s, format M.2 2280.", "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?w=500&auto=format&fit=crop&q=60" },
                    { 10, 499.00m, 22, "Dyski i pamięci", null, "Pamięć RAM DDR5 32GB (2x16GB) 6000MHz", "Niskie opóźnienia CL30, radiatory odprowadzające ciepło, profil EXPO/XMP.", "https://images.unsplash.com/photo-1562976540-1502c2145186?w=500&auto=format&fit=crop&q=60" },
                    { 11, 2799.00m, 7, "Podzespoły PC", null, "Karta graficzna RTX 4070 SUPER 12GB", "Architektura Ada Lovelace, wsparcie DLSS 3, wydajne potrójne chłodzenie.", "https://images.unsplash.com/photo-1591488320449-011701bb6704?w=500&auto=format&fit=crop&q=60" },
                    { 13, 1349.00m, 14, "Podzespoły PC", null, "Procesor 8-rdzeniowy 5.0 GHz", "16 wątków, duża pamięć cache L3, doskonała wydajność w grach.", "https://images.unsplash.com/photo-1555680202-c86f0e12f086?w=500&auto=format&fit=crop&q=60" },
                    { 14, 529.00m, 11, "Podzespoły PC", null, "Chłodzenie wodne AiO 360mm RGB", "Trzy ciche wentylatory 120mm, miedziana podstawa bloku wodnego.", "https://images.unsplash.com/photo-1587202372775-e229f172b9d7?w=500&auto=format&fit=crop&q=60" },
                    { 17, 799.00m, 13, "Podzespoły PC", null, "Płyta główna B650 Gaming Wi-Fi", "Mocna sekcja zasilania VRM, slot PCIe 5.0 M.2, moduł Wi-Fi 6E.", "https://images.unsplash.com/photo-1518770660439-4636190af475?w=500&auto=format&fit=crop&q=60" },
                    { 21, 849.00m, 7, "Gaming", null, "Fotel Gamingowy Ergonomiczny", "Regulowane podłokietniki 3D, poduszka lędźwiowa z pianki memory, tkanina oddychająca.", "https://images.unsplash.com/photo-1598550476439-6847785fcea6?w=500&auto=format&fit=crop&q=60" },
                    { 23, 329.00m, 15, "Audio", null, "Głośniki komputerowe 2.1 z subwooferem", "Moc 80W RMS, głęboki bas, łączność bezprzewodowa Bluetooth i pilot przewodowy.", "https://images.unsplash.com/photo-1545454675-3531b543be5d?w=500&auto=format&fit=crop&q=60" },
                    { 25, 189.00m, 30, "Gaming", null, "Pad bezprzewodowy do gier PC", "Precyzyjne gałki z efektem Halla, wibracje haptyczne, odbiornik USB 2.4GHz.", "https://images.unsplash.com/photo-1600080972464-8e5f35f63d08?w=500&auto=format&fit=crop&q=60" },
                    { 27, 249.00m, 20, "Sieci", null, "Router Wi-Fi 6 AX3000", "Dwuzakresowy, 4 anteny zewnętrzne, obsługa technologii EasyMesh.", "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?w=500&auto=format&fit=crop&q=60" },
                    { 34, 99.00m, 25, "Laptopy", null, "Podstawka chłodząca pod laptopa", "Dwa duże wentylatory 140mm, regulacja kąta nachylenia, podświetlenie LED.", "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=500&auto=format&fit=crop&q=60" },
                    { 35, 149.00m, 30, "Laptopy", null, "Plecak na laptopa 15.6'' wodoodporny", "Dedykowana przegroda na komputer, port ładowania USB, system antykradzieżowy.", "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500&auto=format&fit=crop&q=60" },
                    { 36, 4599.00m, 5, "Laptopy", null, "Laptop Gamingowy 15.6'' 144Hz", "Procesor i7, 16GB RAM, RTX 4060 8GB, dysk SSD 512GB, podświetlana klawiatura.", "https://images.unsplash.com/photo-1603302576837-37561b2e2302?w=500&auto=format&fit=crop&q=60" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProdukt",
                keyValue: 36);
        }
    }
}
