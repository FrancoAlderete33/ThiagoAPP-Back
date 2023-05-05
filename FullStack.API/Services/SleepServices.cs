using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Services
{
    public class SleepServices : ISleepServices
    {
        readonly FullStackDbContext _dbContext;

        public SleepServices(FullStackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Sleep>> GetAllSleeps()
        {
            return await _dbContext.Sleeps.ToListAsync();
        }

        public async Task<Sleep> GetSleepById(int id)
        {
            return await _dbContext.Sleeps.FindAsync(id);
        }


        public async Task<List<Sleep>> GetSleepsByToday(string clientTimeZone)
        {
            var now = DateTime.Now;
            var todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var sleeps = await _dbContext.Sleeps
                                    .Where(b => b.date >= todayStart && b.date <= todayEnd)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            foreach (var sleep in sleeps)
            {
                sleep.start_time = TimeZoneInfo.ConvertTimeFromUtc(sleep.start_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
                sleep.end_time = TimeZoneInfo.ConvertTimeFromUtc(sleep.end_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            }

            return sleeps;
        }
        public async Task<Sleep> CreateSleep(Sleep sleep)
        {
            _dbContext.Sleeps.Add(sleep);
            await _dbContext.SaveChangesAsync();
            return sleep;
        }

        public async Task UpdateSleep(Sleep sleep)
        {
            _dbContext.Sleeps.Update(sleep);
            await _dbContext.SaveChangesAsync();
        }



        public async Task DeleteSleep(int id)
        {
            var sleep = await _dbContext.Sleeps.FindAsync(id);

            if (sleep != null)
            {
                _dbContext.Sleeps.Remove(sleep);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}

