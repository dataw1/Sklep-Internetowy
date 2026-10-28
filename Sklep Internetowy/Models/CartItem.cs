namespace Sklep_Internetowy.Models
{
    public class CartItem
    {
        public int IdProdukt { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public decimal Suma => Cena * Ilosc;
    }
}