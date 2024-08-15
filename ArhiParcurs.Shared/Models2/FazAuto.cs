using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArhiParcurs.Shared.Models;

public partial class FazAuto
{

    [Key]
    public int Arid { get; set; }
    public int Ida { get; set; }

    public string Nrauto { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int Formula { get; set; }

    public int Motor { get; set; }

    public decimal Cm { get; set; }

    public decimal Ca { get; set; }

    public decimal Cb { get; set; }

    public decimal Cf { get; set; }

    public int Ispec { get; set; }

    public int Iaerod { get; set; }

    public int Kmef { get; set; }

    public int Kmefinc { get; set; }

    public int Kmechiv { get; set; }

    public decimal Restrez { get; set; }

    public int Rap { get; set; }

    public int Apar { get; set; }

    public DateTime Arstamp { get; set; }

    public DateTime Aractstamp { get; set; }

    public int Arstatus { get; set; }


}
