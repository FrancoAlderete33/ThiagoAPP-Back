using FullStack.API.Models;

namespace FullStack.API.Services
{
    public interface IBreastfeedingServices
    {
        Task<List<Breastfeeding>> GetAllBreastfeedings();
        Task<List<Breastfeeding>> GetBreastfeedingByToday(string clientTimeZone);
        Task<Breastfeeding> GetBreastfeedingById(int id);
        Task<Breastfeeding> CreateBreastfeeding(Breastfeeding breastfeeding);
        Task UpdateBreastfeeding(Breastfeeding breastfeeding);
        Task DeleteBreastfeeding(int id);

    }
}
