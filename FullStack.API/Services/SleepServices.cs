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
            var nowUtc = DateTime.UtcNow;
            var clientTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone);
            var todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, 0, 0, 0), clientTimeZoneInfo);
            var todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, 23, 59, 59), clientTimeZoneInfo);

            var sleeps = await _dbContext.Sleeps
                .Where(b => b.date >= todayStart && b.date <= todayEnd)
                .OrderByDescending(b => b.date)
                .ToListAsync();

            foreach (var sleep in sleeps)
            {
                sleep.start_time = TimeZoneInfo.ConvertTimeFromUtc(sleep.start_time, clientTimeZoneInfo);
                sleep.end_time = TimeZoneInfo.ConvertTimeFromUtc(sleep.end_time, clientTimeZoneInfo);
            }

            return sleeps;
        }

        public async Task<List<Sleep>> GetSleepsByDate(DateTime date, string clientTimeZone)
        {
            var dateStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var dateEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var sleeps = await _dbContext.Sleeps
                                    .Where(b => b.date >= dateStart && b.date <= dateEnd)
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
            await _dbContext.Sleeps.AddAsync(sleep);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(sleep);
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

