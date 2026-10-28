using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class Produkt
    {
        [Key]
        public int IdProdukt { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        [MaxLength(100)]
        public string Nazwa { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Opis { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cena { get; set; }

        public string ZdjecieUrl { get; set; } = "https://placehold.co/400x300?text=Brak+Zdjecia";

        public string Kategoria { get; set; } = "Ogólne";

        public int IloscWMagazynie { get; set; } = 10;
    }
}