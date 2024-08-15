using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazPiese
{
    public int Idp { get; set; }

    public int Ida { get; set; }

    public string Act { get; set; } = null!;

    public string Denp { get; set; } = null!;

    public decimal Valoare { get; set; }

    public string Zi { get; set; } = null!;

    public string Luna { get; set; } = null!;

    public string An { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
