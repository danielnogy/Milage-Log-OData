using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using System.Net.Http.Json;

namespace ArhiParcurs.Client.Services;

public class SheetRouteService : ISheetRouteService
{
    private readonly HttpClient httpClient;

    public SheetRouteService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<SheetRoute>> GetSheetRoutes()
    {
        var response = await httpClient.GetFromJsonAsync<ODataResponse<SheetRoute>>("api/sheetRoutes");
        return response.Value;
    }

    public async Task<SheetRoute> PostSheetRoute(SheetRoute newSheetRoute)
    {
        var response = await httpClient.PostAsJsonAsync("api/sheetRoutes", newSheetRoute);
        response.EnsureSuccessStatusCode();

        var createdSheetRoute = await response.Content.ReadFromJsonAsync<SheetRoute>();
        return createdSheetRoute;
    }

    public async Task<bool> UpdateSheetRoute(int id, SheetRoute updatedSheetRoute)
    {
        var response = await httpClient.PutAsJsonAsync($"api/sheetRoutes({id})", updatedSheetRoute);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSheetRoute(int id)
    {
        var response = await httpClient.DeleteAsync($"api/sheetRoutes({id})");
        return response.IsSuccessStatusCode;
    }

}
