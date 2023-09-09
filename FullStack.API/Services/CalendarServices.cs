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
            DateTime now = DateTime.Now;
            DateTime todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            DateTime todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            List<Calendar> events = await _dbContext.Calendars
                                        .Where(x => x.timeEventStart >= todayStart && x.timeEventStart <= todayEnd)
                                        .OrderByDescending(x => x.timeEventStart)
                                        .ToListAsync();

            return events;
        }

        public async Task<List<Calendar>> GetEventsByDate(DateTime date, string clientTimeZone)
        {
            DateTime dateStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            DateTime dateEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            List<Calendar> events = await _dbContext.Calendars 
                                    .Where(b => b.date >= dateStart && b.date <= dateEnd)
                                    .OrderByDescending(b => b.date)
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
            Calendar calendar = await _dbContext.Calendars.FindAsync(id);
            if (calendar != null)
            {
                _dbContext.Calendars.Remove(calendar);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
