using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class ConsumptionIncreaseService : IConsumptionIncreaseService
{
    private readonly HttpClient httpClient;

    public ConsumptionIncreaseService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ConsumptionIncrease>> GetConsumptionIncreases()
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<ConsumptionIncrease>>("api/consumptionIncreases");
        return response.Value;
    }

    public async Task<ConsumptionIncrease> PostConsumptionIncrease(ConsumptionIncrease newConsumptionIncrease)
    {
        var response = await httpClient.PostAsJsonAsync("api/consumptionIncreases", newConsumptionIncrease);
        response.EnsureSuccessStatusCode();

        var createdConsumptionIncrease = await response.Content.ReadFromJsonAsync<ConsumptionIncrease>();
        return createdConsumptionIncrease;
    }

    public async Task<bool> UpdateConsumptionIncrease(int id, ConsumptionIncrease updatedConsumptionIncrease)
    {
        var response = await httpClient.PutAsJsonAsync($"api/consumptionIncreases({id})", updatedConsumptionIncrease);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteConsumptionIncrease(int id)
    {
        var response = await httpClient.DeleteAsync($"api/consumptionIncreases({id})");
        return response.IsSuccessStatusCode;
    }

}
