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
            return await _dbContext.BowelMovements.FindAsync(id);
        }

        public async Task<List<BowelMovement>> GetBowelMovementByToday(string clientTimeZone)
        {
            var now = DateTime.Now;
            var todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var bowelMovements = await _dbContext.BowelMovements
                                        .Where(x => x.time >= todayStart && x.time <= todayEnd)
                                        .OrderByDescending(x => x.time)
                                        .ToListAsync();

            return bowelMovements;
        }

        public async Task<List<BowelMovement>> GetBowelMovementByDate(DateTime date, string clientTimeZone)
        {
            var dateStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var dateEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var bowelmovements = await _dbContext.BowelMovements
                                    .Where(b => b.date >= dateStart && b.date <= dateEnd)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            return bowelmovements;
        }


        public async Task<BowelMovement> CreateBowelMovement(BowelMovement bowelMovement)
        {
            await _dbContext.BowelMovements.AddAsync(bowelMovement);
            await _dbContext.SaveChangesAsync();
            return bowelMovement;
        }

        public async Task UpdateBowelMovement(int id, BowelMovement bowelMovement)
        {
            _dbContext.BowelMovements.Update(bowelMovement);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteBowelMovement(int id)
        {
            var bowelMovement = await _dbContext.BowelMovements.FindAsync(id);
            if (bowelMovement != null)
            {
                _dbContext.BowelMovements.Remove(bowelMovement);
                await _dbContext.SaveChangesAsync();
            }

        }
       
    }
}
