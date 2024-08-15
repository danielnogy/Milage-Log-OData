using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface IVehicleService
{
    Task<bool> DeleteVehicle(int id);
    Task<List<Vehicle>> GetVehicles(string odataQuery = "");
    Task<Vehicle> PostVehicle(Vehicle newVehicle);
    Task<bool> UpdateVehicle(int id, Vehicle updatedVehicle);
}