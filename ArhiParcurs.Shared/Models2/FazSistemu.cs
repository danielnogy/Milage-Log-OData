using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazSistemu
{
    public int Idul { get; set; }

    public string Autom { get; set; } = null!;

    public decimal Baia { get; set; }

    public decimal Filtru { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
