using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class SheetService : ISheetService
{
    private readonly HttpClient httpClient;

    public SheetService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Sheet>> GetSheets(string odataQuery = "")
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<Sheet>>("api/sheets"+odataQuery);
        return response.Value;
    }

    public async Task<Sheet> PostSheet(Sheet newSheet)
    {
        var response = await httpClient.PostAsJsonAsync("api/sheets", newSheet);
        response.EnsureSuccessStatusCode();

        var createdSheet = await response.Content.ReadFromJsonAsync<Sheet>();
        return createdSheet;
    }

    public async Task<bool> UpdateSheet(int id, Sheet updatedSheet)
    {
        var response = await httpClient.PutAsJsonAsync($"api/sheets({id})", updatedSheet);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSheet(int id)
    {
        var response = await httpClient.DeleteAsync($"api/sheets({id})");
        return response.IsSuccessStatusCode;
    }

}
