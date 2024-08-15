using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class FuelService : IFuelService
{
    private readonly HttpClient httpClient;

    public FuelService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Fuel>> GetFuels()
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<Fuel>>("api/fuels");
        return response.Value;
    }

    public async Task<Fuel> PostFuel(Fuel newFuel)
    {
        var response = await httpClient.PostAsJsonAsync("api/fuels", newFuel);
        response.EnsureSuccessStatusCode();

        var createdFuel = await response.Content.ReadFromJsonAsync<Fuel>();
        return createdFuel;
    }

    public async Task<bool> UpdateFuel(int id, Fuel updatedFuel)
    {
        var response = await httpClient.PutAsJsonAsync($"api/fuels({id})", updatedFuel);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteFuel(int id)
    {
        var response = await httpClient.DeleteAsync($"api/fuels({id})");
        return response.IsSuccessStatusCode;
    }

}
