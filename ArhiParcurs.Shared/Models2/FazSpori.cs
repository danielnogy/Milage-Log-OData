using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazSpori
{
    public string FelAuto { get; set; } = null!;

    public string FelPrest { get; set; } = null!;

    public decimal Sporkm { get; set; }

    public int Nri { get; set; }

    public string FelPrest2 { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
