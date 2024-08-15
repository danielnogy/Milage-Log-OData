namespace ArhiParcurs.Shared.Models
{
    public class Refuel : BaseDomain
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string NrDocument { get; set; }
        public Provider? Provider { get; set; }
        public int ProviderId { get; set; }
        public int FuelId { get; set; }
        public Fuel? Fuel { get; set; }
        public int SheetId { get; set; }
        public Sheet? Sheet { get; set; }
        public decimal Quantity { get; set; }
        public decimal FuelPrice { get; set; }
        public decimal PriceWithoutTva { get; set; }
        public decimal PriceWithTva { get;set; }
    }
}
