using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;


        }

        public async Task<Walk> DeleteWalkById(Guid id)
        {
            var existingValue = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingValue == null) {
                return null;
            }
            dbContext.Walks.Remove(existingValue);
            await dbContext.SaveChangesAsync();
            return existingValue;
        }

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null)
        {

            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filter

            if (filterOn != null) {}


            return await walks.ToListAsync();
            //var data = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //return data.ToList();
        }

        public async Task<Walk> GetWalkIdAsync(Guid Id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existyingWalk = await dbContext.Walks.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (existyingWalk == null)
            {
                return null;
            }

            existyingWalk.Name = walk.Name;
            existyingWalk.Description = walk.Description;
            existyingWalk.LengthInKm = walk.LengthInKm;
            existyingWalk.WalkImageUrl = walk.WalkImageUrl;
            existyingWalk.DifficultyId = walk.DifficultyId; 
            existyingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existyingWalk;
        }
    }
}
