using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArhiParcurs.Shared.Models;

public partial class FazFoaie
{
    [Key]
    public int Arid { get; set; }
    public int Idf { get; set; }

    public int Nrf { get; set; }

    public int Ida { get; set; }

    public string Sofer { get; set; } = null!;

    public decimal Orap { get; set; }

    public decimal Oras { get; set; }

    public decimal Nrore { get; set; }

    public string Data { get; set; } = null!;

    public int Decada { get; set; }

    public int Idl { get; set; }

    public int Anul { get; set; }

    public decimal Bm { get; set; }

    public decimal Gz { get; set; }

    public decimal Um { get; set; }

    public decimal Ut { get; set; }

    public decimal Ua { get; set; }

    public decimal Schu { get; set; }

    public decimal Schf { get; set; }

    public string Nractschu { get; set; } = null!;

    public string Nractschf { get; set; } = null!;

    public decimal Restit { get; set; }

    public string Nractc { get; set; } = null!;

    public string Nractrst { get; set; } = null!;

    public string Nractu1 { get; set; } = null!;

    public string Nractu2 { get; set; } = null!;

    public string Nractu3 { get; set; } = null!;

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }

}
