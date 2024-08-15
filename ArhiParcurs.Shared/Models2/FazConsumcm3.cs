using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazConsumcm3
{
    public int Idcm3 { get; set; }

    public string Fel { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Loc { get; set; } = null!;

    public int Capcil { get; set; }

    public decimal Cm { get; set; }

    public int Aprind { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
