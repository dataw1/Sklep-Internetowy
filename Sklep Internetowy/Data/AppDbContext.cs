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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja dziedziczenia w bazie danych (wzorzec TPH - Table Per Hierarchy)
            modelBuilder.Entity<Uzytkownik>()
                .HasDiscriminator<string>("Rola")
                .HasValue<Klient>("Klient")
                .HasValue<Administrator>("Administrator");
        }
    }
}