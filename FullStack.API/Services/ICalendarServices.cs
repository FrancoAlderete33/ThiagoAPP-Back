using FullStack.API.Models;

namespace FullStack.API.Services
{
    public interface ICalendarServices
    {
        Task<List<Calendar>> GetAllEvents();
        Task<Calendar> GetEventById(int id);
        Task<List<Calendar>> GetEventsByToday(string clientTimeZone);
        Task<List<Calendar>> GetEventsByDate(DateTime date, string clientTimeZone);
        Task<Calendar> CreateEvent(Calendar calendar);
        Task<Calendar> UpdateEvent(Calendar calendar);
        Task DeleteEvent(int id);
    }
}
