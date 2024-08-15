using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazLuna
{
    public int Idl { get; set; }

    public string Denluna { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
