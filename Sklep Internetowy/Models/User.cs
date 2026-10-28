using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    // Klasa abstrakcyjna zgodna z diagramem UML
    public abstract class Uzytkownik
    {
        [Key]
        public int IdUzytkownik { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string HasloHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon jest wymagany")]
        public string Telefon { get; set; } = string.Empty;

        public DateTime DataRejestracji { get; set; } = DateTime.UtcNow;
    }

    // Klasa Klient dziedzicząca po Uzytkownik (Generalizacja)
    public class Klient : Uzytkownik
    {
        [Required(ErrorMessage = "Ulica jest wymagana")]
        public string Ulica { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer domu jest wymagany")]
        public string NumerDomu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Format kodu pocztowego to XX-XXX")]
        public string KodPocztowy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string Miasto { get; set; } = string.Empty;
    }

    // Klasa Administrator dziedzicząca po Uzytkownik (Generalizacja)
    public class Administrator : Uzytkownik
    {
        public string PoziomUprawnien { get; set; } = " Pelny";
    }
}