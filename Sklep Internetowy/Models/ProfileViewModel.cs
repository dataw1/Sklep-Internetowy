using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class ProfilViewModel
    {
        [Display(Name = "Adres Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer domu jest wymagany")]
        [Display(Name = "Numer domu")]
        public string NumerDomu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Format: 00-000")]
        [Display(Name = "Kod pocztowy")]
        public string KodPocztowy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        [Display(Name = "Miasto")]
        public string Miasto { get; set; } = string.Empty;
    }
}