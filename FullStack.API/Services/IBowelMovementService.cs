using FullStack.API.Models;

namespace FullStack.API.Services
{
    public interface IBowelMovementService
    {
        Task<List<BowelMovement>> GetAllBowelMovements();
        Task<BowelMovement> CreateBowelMovement(BowelMovement bowelMovement);

        Task<List<BowelMovement>> GetBowelMovementByToday(string clientTimeZone);
        Task<BowelMovement> GetBowelMovementsById(int id);
        Task DeleteBowelMovement(int id);
        Task UpdateBowelMovement(int id, BowelMovement bowelMovement);
    }
}
