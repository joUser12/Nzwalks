﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public async  Task<Region> CreateAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
            

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
          
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);

            if (existingRegion == null) {
                return null;

            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           var querey=  dbContext.Regions.AsQueryable();
          return querey.ToList();
        }

  

       public async Task<Region?> GetByIdAsync(Guid id)
        {
          return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

   
        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
          var existingRegion =await dbContext.Regions.FirstOrDefaultAsync(y=>y.Id == Id);
            if (existingRegion == null) {
                return null;
            
            }
            existingRegion.Code=region.Code;
            existingRegion.Name=region.Name;
            existingRegion.RegionImageUrl=region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
