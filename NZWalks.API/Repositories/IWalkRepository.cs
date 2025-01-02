using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalkAsync(string ? filterOn =  null ,string? filterQuery = null);

       Task <Walk> GetWalkIdAsync(Guid Id);

       Task<Walk ?>  UpdateAsync(Guid id, Walk walk);

       Task<Walk> DeleteWalkById(Guid id);


    }
}
