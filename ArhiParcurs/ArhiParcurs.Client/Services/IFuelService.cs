using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface IFuelService
{
    Task<bool> DeleteFuel(int id);
    Task<List<Fuel>> GetFuels();
    Task<Fuel> PostFuel(Fuel newFuel);
    Task<bool> UpdateFuel(int id, Fuel updatedFuel);
}