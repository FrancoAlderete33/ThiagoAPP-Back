using FullStack.API.Models;

namespace FullStack.API.Services
{
    public interface IBreastfeedingServices
    {
        Task<List<Breastfeeding>> GetAllBreastfeedings();
        Task<List<Breastfeeding>> GetBreastfeedingByToday(string clientTimeZone);
        Task<List<Breastfeeding>> GetBreastfeedingByDate(DateTime date, string clientTimeZone);
        Task<Breastfeeding> GetBreastfeedingById(int id);

        //Task<int> GetTotalDurationToday(string clientTimeZone);
        Task<Breastfeeding> CreateBreastfeeding(Breastfeeding breastfeeding);
        Task UpdateBreastfeeding(Breastfeeding breastfeeding);
        Task DeleteBreastfeeding(int id);

    }
}
