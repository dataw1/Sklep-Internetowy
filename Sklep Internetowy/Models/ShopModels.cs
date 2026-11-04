using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public enum StatusZamowienia
    {
        OCZEKUJACE,
        WYSYLANE,
        ZREALIZOWANE,
        ANULOWANE,
        OPLACONE
    }

    public class Kategoria
    {
        [Key]
        public int IdKategorii { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        public string Nazwa { get; set; } = string.Empty;

        public string Opis { get; set; } = string.Empty;

        public List<Produkt> Produkty { get; set; } = new();
    }

    public class Produkt1
    {
        [Key]
        public int IdProduktu { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        [Display(Name = "Nazwa produktu")]
        public string Nazwa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Opis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena netto jest wymagana")]
        [Range(0.01, 100000, ErrorMessage = "Cena musi być większa od 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena Netto (zł)")]
        public decimal CenaNetto { get; set; }

        [Required(ErrorMessage = "Stawka VAT jest wymagana")]
        [Range(0, 100, ErrorMessage = "Podaj wartość procentową (np. 23)")]
        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Stawka VAT (%)")]
        public decimal StawkaVat { get; set; } = 23m;

        [Required(ErrorMessage = "Stan magazynowy jest wymagany")]
        [Range(0, 100000, ErrorMessage = "Stan nie może być ujemny")]
        [Display(Name = "Stan magazynowy (szt.)")]
        public int StanMagazynowy { get; set; }

        [Display(Name = "URL Zdjęcia")]
        public string UrlZdjecia { get; set; } = "/images/default-product.png";

        [Display(Name = "Czy produkt jest aktywny?")]
        public bool CzyAktywny { get; set; } = true;

        public int? IdKategorii { get; set; }
        [ForeignKey("IdKategorii")]
        public Kategoria? Kategoria { get; set; }

        // Metoda z diagramu UML obliczająca cenę brutto
        public decimal PobierzCeneBrutto()
        {
            return Math.Round(CenaNetto * (1 + (StawkaVat / 100m)), 2);
        }

        // Metoda z diagramu UML aktualizująca stan
        public void AktualizujStan(int ilosc)
        {
            StanMagazynowy += ilosc;
        }
    }

    public class Zamowienie
    {
        [Key]
        public int IdZamowienia { get; set; }

        public DateTime DataZlozenia { get; set; } = DateTime.UtcNow;

        public StatusZamowienia Status { get; set; } = StatusZamowienia.OCZEKUJACE;

        [Column(TypeName = "decimal(18,2)")]
        public decimal LacznyKoszt { get; set; }

        public string AdresDostawyUlica { get; set; } = string.Empty;
        public string AdresDostawyKod { get; set; } = string.Empty;
        public string AdresDostawyMiasto { get; set; } = string.Empty;
        public string MetodaPlatnosci { get; set; } = string.Empty;
        public string MetodaDostawy { get; set; } = string.Empty;

        public int? KlientId { get; set; }
        [ForeignKey("KlientId")]
        public Klient? Klient { get; set; }
    }
}