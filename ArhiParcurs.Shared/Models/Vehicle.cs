using ArhiParcurs.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhiParcurs.Shared.Models;
public class Vehicle : BaseDomain
{
    public string Number { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string VIN { get; set; } //vehicle identification number
    public decimal InitialFuelTank { get; set; } = 0;
    public decimal FuelConsumption { get; set; }
    public int Kilometers { get; set; }
    public Fuel Fuel { get; set; }
    public decimal FuelLeft { get; set; }
    public int FuelId { get; set; }
    public int? VehicleState { get; set; } = (int)VehicleStateEnum.Activ;
    public int? BranchId { get; set; }
    public Branch? Branch { get; set; }
}
