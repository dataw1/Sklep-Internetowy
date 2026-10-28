using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class RejestracjaViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
        public string Haslo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon jest wymagany")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ulica jest wymagana")]
        public string Ulica { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer domu jest wymagany")]
        public string NumerDomu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi być w formacie XX-XXX")]
        public string KodPocztowy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string Miasto { get; set; } = string.Empty;
    }

    public class LogowanieViewModel
    {
        [Required(ErrorMessage = "Podaj adres email")]
        [EmailAddress(ErrorMessage = "Niepoprawny format email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        public string Haslo { get; set; } = string.Empty;
    }
}