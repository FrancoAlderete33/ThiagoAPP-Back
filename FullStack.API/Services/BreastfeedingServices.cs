﻿using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Services
{
    public class BreastfeedingServices : IBreastfeedingServices
    {
        readonly FullStackDbContext _dbContext;

        public BreastfeedingServices(FullStackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Breastfeeding>> GetAllBreastfeedings()
        {
            return await _dbContext.Breastfeedings.ToListAsync();
        }

        public async Task<Breastfeeding> GetBreastfeedingById(int id)
        {
            return await _dbContext.Breastfeedings.FindAsync(id);
        }


        public async Task<List<Breastfeeding>> GetBreastfeedingByToday(string clientTimeZone)
        {
            var now = DateTime.Now;
            var todayStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var todayEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(now.Year, now.Month, now.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var breastfeedings = await _dbContext.Breastfeedings
                                    .Where(b => b.date >= todayStart && b.date <= todayEnd)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            foreach (var breastfeeding in breastfeedings)
            {
                breastfeeding.start_time = TimeZoneInfo.ConvertTimeFromUtc(breastfeeding.start_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
                breastfeeding.end_time = TimeZoneInfo.ConvertTimeFromUtc(breastfeeding.end_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            }

            return breastfeedings;
        }

        public async Task<List<Breastfeeding>> GetBreastfeedingByDate(DateTime date, string clientTimeZone)
        {
            var dateStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var dateEnd = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59), TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            var breastfeedings = await _dbContext.Breastfeedings
                                    .Where(b => b.date >= dateStart && b.date <= dateEnd)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            foreach (var breastfeeding in breastfeedings)
            {
                breastfeeding.start_time = TimeZoneInfo.ConvertTimeFromUtc(breastfeeding.start_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
                breastfeeding.end_time = TimeZoneInfo.ConvertTimeFromUtc(breastfeeding.end_time, TimeZoneInfo.FindSystemTimeZoneById(clientTimeZone));
            }

            return breastfeedings;
        }

        public async Task<Breastfeeding> CreateBreastfeeding(Breastfeeding breastfeeding)
        {
            await _dbContext.Breastfeedings.AddAsync(breastfeeding);
            await _dbContext.SaveChangesAsync();
            return breastfeeding;
        }

        public async Task UpdateBreastfeeding(Breastfeeding breastfeeding)
        {
            _dbContext.Breastfeedings.Update(breastfeeding);
            await _dbContext.SaveChangesAsync();
        }



        public async Task DeleteBreastfeeding(int id)
        {
            var breastfeeding = await _dbContext.Breastfeedings.FindAsync(id);

            if (breastfeeding != null)
            {
                _dbContext.Breastfeedings.Remove(breastfeeding);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}

