using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface ISheetService
{
    Task<bool> DeleteSheet(int id);
    Task<List<Sheet>> GetSheets(string odataQuery="");
    Task<Sheet> PostSheet(Sheet newSheet);
    Task<bool> UpdateSheet(int id, Sheet updatedSheet);
}