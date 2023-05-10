using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Services
{
    public class CalendarServices : ICalendarServices
    {
        readonly FullStackDbContext _dbContext;

        public CalendarServices(FullStackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Calendar>> GetAllEvents()
        {
            return await _dbContext.Calendars.ToListAsync();
        }

        public async Task<Calendar> GetEventById(int id)
        {
            return await _dbContext.Calendars.FirstAsync(c => c.Id == id);
        }
        public async Task<List<Calendar>> GetEventsByToday(string clientTimeZone)
        {
            var now = DateTime.Now;
            var todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var events = await _dbContext.Calendars
                                        .Where(x => x.time >= todayStart && x.time <= todayEnd)
                                        .OrderByDescending(x => x.time)
                                        .ToListAsync();

            return events;
        }

        public async Task<Calendar> CreateEvent(Calendar calendar)
        {
            await _dbContext.Calendars.AddAsync(calendar);
            await _dbContext.SaveChangesAsync();
            return calendar;
        }

        public async Task<Calendar> UpdateEvent(Calendar calendar)
        {
            _dbContext.Calendars.Update(calendar);
            await _dbContext.SaveChangesAsync();
            return calendar;
        }
    

    public async Task DeleteEvent(int id)
        {
           var calendar = await _dbContext.Calendars.FindAsync(id);
            if (calendar != null)
            {
                _dbContext.Calendars.Remove(calendar);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
