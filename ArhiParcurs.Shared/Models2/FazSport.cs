using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazSport
{
    public string Nrcrt { get; set; } = null!;

    public string Gr { get; set; } = null!;

    public int Motor1r1 { get; set; }

    public int Motor1r2 { get; set; }

    public int Motor2r1 { get; set; }

    public int Motor2r2 { get; set; }

    public int Motor3r1 { get; set; }

    public int Motor3r2 { get; set; }

    public string TipR { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
