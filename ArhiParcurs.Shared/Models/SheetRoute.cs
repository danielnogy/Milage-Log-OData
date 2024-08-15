using ArhiParcurs.Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArhiParcurs.Shared.Models;
public class SheetRoute : BaseDomain
{
    public DateTime StartDate { get; set; } = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second).AddHours(8);
    public DateTime EndDate { get; set; } = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second).AddHours(16);
    public string? Start { get; set; }
    public string? End { get; set; }
    public string VIA { get; set; } = string.Empty;
    public decimal TotalDistance { get; set; }
    public decimal TotalEquivalentDistance { get; set; }
    public decimal Distance1 { get; set; }
    public decimal Distance2 { get; set; }
    public decimal Distance3 { get; set; }
    public decimal Distance4 { get; set; }
    public decimal Distance5 { get; set; }
    public decimal Distance6 { get; set; }
    public string Purpose { get; set; } = string.Empty;

    public bool RoundTrip { get; set; } = false;
    public int? ConsumptionIncreaseId { get; set; }
    public ConsumptionIncrease? ConsumptionIncrease { get; set; }

    public int SheetId { get; set; }
    public Sheet? Sheet { get; set; }

}
