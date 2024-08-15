using ArhiParcurs.Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArhiParcurs.Shared.Models;
public class Partner : BaseDomain
{
    [Required(ErrorMessage ="Numele este obligatoriu")]
    public string Name { get; set; }
    [EmailAddress(ErrorMessage ="Adresa de email invalida")]
    public string Email { get; set; }
    public string Function { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    [Required(ErrorMessage = "CNP obligatoriu")]
    public string SSN { get; set; }//cnp sau social securty number(SSN)
    public int? PartnerState { get; set; } = (int)PartnerStateEnum.Activ;
}
