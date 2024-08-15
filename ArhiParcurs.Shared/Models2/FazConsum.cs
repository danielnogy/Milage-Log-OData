using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazConsum
{
    public int Idcm { get; set; }

    public int Ida { get; set; }

    public decimal Cm1 { get; set; }

    public decimal Cm2 { get; set; }

    public decimal Cm3 { get; set; }

    public int Implicit { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
