using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface IPartnerService
{
    Task<bool> DeletePartner(int id);
    Task<List<Partner>> GetPartners();
    Task<Partner> PostPartner(Partner newPartner);
    Task<bool> UpdatePartner(int id, Partner updatedPartner);
}