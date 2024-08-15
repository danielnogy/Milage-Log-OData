using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface ISheetRouteService
{
    Task<bool> DeleteSheetRoute(int id);
    Task<List<SheetRoute>> GetSheetRoutes();
    Task<SheetRoute> PostSheetRoute(SheetRoute newSheetRoute);
    Task<bool> UpdateSheetRoute(int id, SheetRoute updatedSheetRoute);
}