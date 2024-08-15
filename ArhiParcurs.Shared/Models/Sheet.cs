using System.ComponentModel.DataAnnotations;

namespace ArhiParcurs.Shared.Models;
public class Sheet:BaseDomain
{
    public int Number { get; set; }
    public decimal InitialFuel { get; set; }
    public decimal Refuels { get; set; }
    public decimal Consumed { get; set; }
    public decimal FuelLeft { get; set; }
    public decimal DrivenKilometers { get; set; }

    public DateTime DateBegin { get; set; } = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second).AddHours(8);
    public DateTime DateEnd { get; set; } = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second).AddHours(16);
    public int? ConsumptionIncreaseId { get; set; }
    public ConsumptionIncrease? ConsumptionIncrease { get; set; }
    [Range(1, int.MaxValue)]
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    [Range(1, int.MaxValue)]
    public int DriverId { get; set; }
    public Partner? Driver { get; set; }
    [Range(1, int.MaxValue)]
    public int CreatorId { get; set; }
    public Partner? Creator { get; set; }
    [Range(1, int.MaxValue)]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }


    public List<SheetRoute> SheetRoutes { get; set; } = new();
    public List<Refuel> RefuelsList { get; set; } = new();

}
