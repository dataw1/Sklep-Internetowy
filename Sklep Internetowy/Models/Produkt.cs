using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class Produkt
    {
        [Key]
        public int IdProdukt { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        [Display(Name = "Nazwa produktu")]
        public string Nazwa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Opis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena jest wymagana")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena (zł)")]
        public decimal Cena { get; set; }

        public string ZdjecieUrl { get; set; } = "/images/default-product.png";

        public string Kategoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ilość w magazynie jest wymagana")]
        [Display(Name = "Ilość w magazynie (szt.)")]
        public int IloscWMagazynie { get; set; }

        // Właściwości ignorowane w bazie danych, ale potrzebne dla UML/Widoków
        [NotMapped]
        public decimal StawkaVat { get; set; } = 23m;

        [NotMapped]
        public bool CzyAktywny { get; set; } = true;

        public decimal PobierzCeneBrutto()
        {
            return Math.Round(Cena * (1 + (StawkaVat / 100m)), 2);
        }

        public void AktualizujStan(int ilosc)
        {
            IloscWMagazynie += ilosc;
        }
    }
}