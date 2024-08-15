using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazSporu
{
    public int Idu { get; set; }

    public string Autom { get; set; } = null!;

    public int Bucur { get; set; }

    public int Municip { get; set; }

    public int Restul { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
