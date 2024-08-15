using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class VehicleService : IVehicleService
{
    private readonly HttpClient httpClient;

    public VehicleService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Vehicle>> GetVehicles(string odataQuery="")
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<Vehicle>>("api/vehicles"+odataQuery);
        return response.Value;
    }

    public async Task<Vehicle> PostVehicle(Vehicle newVehicle)
    {
        var response = await httpClient.PostAsJsonAsync("api/vehicles", newVehicle);
        response.EnsureSuccessStatusCode();

        var createdVehicle = await response.Content.ReadFromJsonAsync<Vehicle>();
        return createdVehicle;
    }

    public async Task<bool> UpdateVehicle(int id, Vehicle updatedVehicle)
    {
        var response = await httpClient.PutAsJsonAsync($"api/vehicles({id})", updatedVehicle);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteVehicle(int id)
    {
        var response = await httpClient.DeleteAsync($"api/vehicles({id})");
        return response.IsSuccessStatusCode;
    }

}
