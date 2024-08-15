using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazSetari
{
    public string Unitate { get; set; } = null!;

    public string Functie { get; set; } = null!;

    public string Numeman { get; set; } = null!;

    public string Intocmit { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
