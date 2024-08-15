using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhiParcurs.Shared.Models
{
    public class Branch : BaseDomain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
