using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhiParcurs.Shared.Models;
public class Fuel : BaseDomain
{
    public string Name { get; set; }
    public decimal TVA { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; }
}
