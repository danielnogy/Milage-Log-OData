namespace ArhiParcurs.Shared.Models
{
    public class Setting:BaseDomain
    {
        public string DenumireFirma { get; set; }
        public string Adresa { get; set; }
        public string NrInreg { get; set; }
        public string CUI { get; set; }
        public bool WithoutRoutes { get; set; }
        public bool WithoutPurpose { get; set; }
        public bool WithoutRoundtrip { get; set; }
    }
}
