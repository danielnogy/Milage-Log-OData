using System;
using System.Collections.Generic;

namespace ArhiParcurs.Shared.Models;

public partial class FazUleica1
{
    public int Idard { get; set; }

    public string Ciclu { get; set; } = null!;

    public decimal Autocamion { get; set; }

    public decimal Autotren { get; set; }

    public decimal Autobuz { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

    public int Arid { get; set; }
}
