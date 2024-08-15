namespace ArhiParcurs.Shared.Models;
public class Provider : BaseDomain
{
    public string Name { get; set; }

    public List<Refuel> Refuels { get; set; }
}
