using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Administrator> Administratorzy { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wzorzec TPH dla użytkowników
            modelBuilder.Entity<Uzytkownik>()
                .HasDiscriminator<string>("Rola")
                .HasValue<Klient>("Klient")
                .HasValue<Administrator>("Administrator");

            // Dane początkowe (Seed) produktów - każdy produkt posiada unikalne zdjęcie
            modelBuilder.Entity<Produkt>().HasData(
                new Produkt
                {
                    IdProdukt = 1,
                    Nazwa = "Klawiatura mechaniczna RGB",
                    Opis = "Przełączniki Red, cicha praca, podświetlenie RGB, układ PL.",
                    Cena = 299.99m,
                    Kategoria = "Akcesoria",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 15
                },
                new Produkt
                {
                    IdProdukt = 2,
                    Nazwa = "Mysz bezprzewodowa Pro",
                    Opis = "Sensor 16000 DPI, ergonomiczny kształt, do 70h na jednym ładowaniu.",
                    Cena = 149.50m,
                    Kategoria = "Akcesoria",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 25
                },
                new Produkt
                {
                    IdProdukt = 3,
                    Nazwa = "Słuchawki Gamingowe 7.1",
                    Opis = "Dźwięk przestrzenny, mikrofon z redukcją szumów, miękkie nauszniki.",
                    Cena = 219.00m,
                    Kategoria = "Audio",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 8
                },
                new Produkt
                {
                    IdProdukt = 4,
                    Nazwa = "Monitor Gamingowy 27'' IPS",
                    Opis = "Rozdzielczość 1440p, odświeżanie 165Hz, czas reakcji 1ms.",
                    Cena = 1099.00m,
                    Kategoria = "Monitory",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 5
                },
                new Produkt
                {
                    IdProdukt = 5,
                    Nazwa = "Podkładka pod mysz XXL",
                    Opis = "Wymiary 900x400 mm, obszyte krawędzie, antypoślizgowa guma.",
                    Cena = 59.99m,
                    Kategoria = "Akcesoria",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 40
                },
                new Produkt
                {
                    IdProdukt = 6,
                    Nazwa = "Kamerka internetowa 4K Pro",
                    Opis = "Autofocus, wbudowany mikrofon stereo, osłona obiektywu.",
                    Cena = 349.00m,
                    Kategoria = "Peryferia",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1588508065123-287b28e013da?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 18
                },
                new Produkt
                {
                    IdProdukt = 7,
                    Nazwa = "Mikrofon pojemnościowy USB",
                    Opis = "Studyjna jakość nagrywania, koszyk antywstrząsowy i pop-filtr.",
                    Cena = 279.90m,
                    Kategoria = "Audio",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1590602847861-f357a9332bbc?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 12
                },
                new Produkt
                {
                    IdProdukt = 8,
                    Nazwa = "Dysk SSD NVMe 1TB PCIe 4.0",
                    Opis = "Odczyt do 7000 MB/s, zapis do 5000 MB/s, format M.2 2280.",
                    Cena = 389.00m,
                    Kategoria = "Dyski i pamięci",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 30
                },
                new Produkt
                {
                    IdProdukt = 10,
                    Nazwa = "Pamięć RAM DDR5 32GB (2x16GB) 6000MHz",
                    Opis = "Niskie opóźnienia CL30, radiatory odprowadzające ciepło, profil EXPO/XMP.",
                    Cena = 499.00m,
                    Kategoria = "Dyski i pamięci",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1562976540-1502c2145186?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 22
                },
                new Produkt
                {
                    IdProdukt = 11,
                    Nazwa = "Karta graficzna RTX 4070 SUPER 12GB",
                    Opis = "Architektura Ada Lovelace, wsparcie DLSS 3, wydajne potrójne chłodzenie.",
                    Cena = 2799.00m,
                    Kategoria = "Podzespoły PC",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1591488320449-011701bb6704?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 7
                },
                new Produkt
                {
                    IdProdukt = 13,
                    Nazwa = "Procesor 8-rdzeniowy 5.0 GHz",
                    Opis = "16 wątków, duża pamięć cache L3, doskonała wydajność w grach.",
                    Cena = 1349.00m,
                    Kategoria = "Podzespoły PC",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1555680202-c86f0e12f086?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 14
                },
                new Produkt
                {
                    IdProdukt = 14,
                    Nazwa = "Chłodzenie wodne AiO 360mm RGB",
                    Opis = "Trzy ciche wentylatory 120mm, miedziana podstawa bloku wodnego.",
                    Cena = 529.00m,
                    Kategoria = "Podzespoły PC",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1587202372775-e229f172b9d7?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 11
                },
                new Produkt
                {
                    IdProdukt = 17,
                    Nazwa = "Płyta główna B650 Gaming Wi-Fi",
                    Opis = "Mocna sekcja zasilania VRM, slot PCIe 5.0 M.2, moduł Wi-Fi 6E.",
                    Cena = 799.00m,
                    Kategoria = "Podzespoły PC",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 13
                },
                new Produkt
                {
                    IdProdukt = 21,
                    Nazwa = "Fotel Gamingowy Ergonomiczny",
                    Opis = "Regulowane podłokietniki 3D, poduszka lędźwiowa z pianki memory, tkanina oddychająca.",
                    Cena = 849.00m,
                    Kategoria = "Gaming",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1598550476439-6847785fcea6?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 7
                },
                new Produkt
                {
                    IdProdukt = 23,
                    Nazwa = "Głośniki komputerowe 2.1 z subwooferem",
                    Opis = "Moc 80W RMS, głęboki bas, łączność bezprzewodowa Bluetooth i pilot przewodowy.",
                    Cena = 329.00m,
                    Kategoria = "Audio",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1545454675-3531b543be5d?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 15
                },
                new Produkt
                {
                    IdProdukt = 25,
                    Nazwa = "Pad bezprzewodowy do gier PC",
                    Opis = "Precyzyjne gałki z efektem Halla, wibracje haptyczne, odbiornik USB 2.4GHz.",
                    Cena = 189.00m,
                    Kategoria = "Gaming",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1600080972464-8e5f35f63d08?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 30
                },
                new Produkt
                {
                    IdProdukt = 27,
                    Nazwa = "Router Wi-Fi 6 AX3000",
                    Opis = "Dwuzakresowy, 4 anteny zewnętrzne, obsługa technologii EasyMesh.",
                    Cena = 249.00m,
                    Kategoria = "Sieci",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 20
                },
                new Produkt
                {
                    IdProdukt = 34,
                    Nazwa = "Podstawka chłodząca pod laptopa",
                    Opis = "Dwa duże wentylatory 140mm, regulacja kąta nachylenia, podświetlenie LED.",
                    Cena = 99.00m,
                    Kategoria = "Laptopy",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 25
                },
                new Produkt
                {
                    IdProdukt = 35,
                    Nazwa = "Plecak na laptopa 15.6'' wodoodporny",
                    Opis = "Dedykowana przegroda na komputer, port ładowania USB, system antykradzieżowy.",
                    Cena = 149.00m,
                    Kategoria = "Laptopy",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 30
                },
                new Produkt
                {
                    IdProdukt = 36,
                    Nazwa = "Laptop Gamingowy 15.6'' 144Hz",
                    Opis = "Procesor i7, 16GB RAM, RTX 4060 8GB, dysk SSD 512GB, podświetlana klawiatura.",
                    Cena = 4599.00m,
                    Kategoria = "Laptopy",
                    ZdjecieUrl = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?w=500&auto=format&fit=crop&q=60",
                    IloscWMagazynie = 5
                }
            );
        }
    }
}