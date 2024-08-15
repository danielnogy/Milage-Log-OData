using ArhiParcurs.Shared.Models;

namespace ArhiParcurs.Client.Services;
public interface IConsumptionIncreaseService
{
    Task<bool> DeleteConsumptionIncrease(int id);
    Task<List<ConsumptionIncrease>> GetConsumptionIncreases();
    Task<ConsumptionIncrease> PostConsumptionIncrease(ConsumptionIncrease newConsumptionIncrease);
    Task<bool> UpdateConsumptionIncrease(int id, ConsumptionIncrease updatedConsumptionIncrease);
}