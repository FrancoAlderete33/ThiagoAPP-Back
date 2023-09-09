using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Services
{
    public class BowelMovementService : IBowelMovementService
    {
        readonly FullStackDbContext _dbContext;

        public BowelMovementService(FullStackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BowelMovement>> GetAllBowelMovements()
        {
            return await _dbContext.BowelMovements.ToListAsync();
        }

        public async Task<BowelMovement> GetBowelMovementsById(int id)
        {
            BowelMovement bowelMovementId = await _dbContext.BowelMovements.FindAsync(id);

            if (bowelMovementId == null)
            {
                throw new Exception($"El Id:{bowelMovementId.Id} no existe");
            }

            return bowelMovementId;
        }

        public async Task<List<BowelMovement>> GetBowelMovementByToday(string clientTimeZone)
        {

            DateTime now = DateTime.Now;

            DateTime todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0),
                                  TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));

            DateTime todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59),
                                TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));

            List<BowelMovement> bowelMovements = await _dbContext.BowelMovements
                                        .Where(x => x.time >= todayStart && x.time <= todayEnd)
                                        .OrderByDescending(x => x.time)
                                        .ToListAsync();

            return bowelMovements;
        }

        public async Task<List<BowelMovement>> GetBowelMovementByDate(DateTime date, string clientTimeZone)
        {
            DateTime dateStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0),
                                 TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));

            DateTime dateEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59),
                               TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));

            List<BowelMovement> bowelmovements = await _dbContext.BowelMovements
                                    .Where(b => b.date >= dateStart && b.date <= dateEnd)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            return bowelmovements;
        }


        public async Task<BowelMovement> CreateBowelMovement(BowelMovement bowelMovement)
        {
            if (bowelMovement == null)
            {
                throw new ArgumentNullException(nameof(bowelMovement));
            }

            await _dbContext.BowelMovements.AddAsync(bowelMovement);
            await _dbContext.SaveChangesAsync();

            return bowelMovement;
        }

        public async Task UpdateBowelMovement(int id, BowelMovement bowelMovement)
        {
            if (bowelMovement == null)
            {
                throw new Exception($"Error al actualizar BowelMovement con Id: {bowelMovement.Id}");
            }

            _dbContext.BowelMovements.Update(bowelMovement);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteBowelMovement(int id)
        {
            BowelMovement bowelMovement = await _dbContext.BowelMovements.FindAsync(id);

            if (bowelMovement == null)
            {
                throw new Exception($"El bowelMovement con Id: {bowelMovement.Id} no existe");
            }

            _dbContext.BowelMovements.Remove(bowelMovement);
            await _dbContext.SaveChangesAsync();
        }

    }
}
