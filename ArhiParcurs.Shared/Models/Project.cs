namespace ArhiParcurs.Shared.Models;
public class Project : BaseDomain
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public List<Sheet> Sheets { get; set; } = new();
}
