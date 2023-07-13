using MongoDB.Driver;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Models;

namespace RealStateMVCWebApp.Service
{
    public class ScheduleTourService
    {
        private readonly IMongoCollection<ScheduleTour> _collection;

        public ScheduleTourService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(MongoDBConnection.DBName);
            _collection = database.GetCollection<ScheduleTour>("schedule_tour");
        }

        public async Task<List<ScheduleTour>> Get()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<ScheduleTour> Get(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(ScheduleTour listing)
        {
            await _collection.InsertOneAsync(listing);
        }

        public async Task Update(ScheduleTour listing)
        {
            await _collection.ReplaceOneAsync(p => p.Id == listing.Id, listing);
        }
        
    }
}
