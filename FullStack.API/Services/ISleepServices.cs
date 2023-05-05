using FullStack.API.Models;

namespace FullStack.API.Services
{
    public interface ISleepServices
    {
        Task<List<Sleep>> GetAllSleeps();
        Task<List<Sleep>> GetSleepsByToday(string clientTimeZone);
        Task<Sleep> GetSleepById(int id);
        Task<Sleep> CreateSleep(Sleep sleep);
        Task UpdateSleep(Sleep sleep);
        Task DeleteSleep(int id);

    }
}
