namespace ArhiParcurs.Shared.Models;
public class ConsumptionIncrease : BaseDomain
{
    public string Name { get; set; }
    public decimal Percent { get; set; }

    public ICollection<Sheet> Sheets { get; set; }
}
