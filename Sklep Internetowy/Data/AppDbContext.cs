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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wzorzec TPH dla użytkowników
            modelBuilder.Entity<Uzytkownik>()
                .HasDiscriminator<string>("Rola")
                .HasValue<Klient>("Klient")
                .HasValue<Administrator>("Administrator");

            // Dane początkowe (Seed) produktów
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
                }
            );
        }
    }
}