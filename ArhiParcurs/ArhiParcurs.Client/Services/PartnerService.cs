using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class PartnerService : IPartnerService
{
    private readonly HttpClient httpClient;

    public PartnerService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Partner>> GetPartners()
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<Partner>>("api/partners");
        return response.Value;
    }

    public async Task<Partner> PostPartner(Partner newPartner)
    {
        var response = await httpClient.PostAsJsonAsync("api/partners", newPartner);
        response.EnsureSuccessStatusCode();

        var createdPartner = await response.Content.ReadFromJsonAsync<Partner>();
        return createdPartner;
    }

    public async Task<bool> UpdatePartner(int id, Partner updatedPartner)
    {
        var response = await httpClient.PutAsJsonAsync($"api/partners({id})", updatedPartner);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePartner(int id)
    {
        var response = await httpClient.DeleteAsync($"api/partners({id})");
        return response.IsSuccessStatusCode;
    }

}
