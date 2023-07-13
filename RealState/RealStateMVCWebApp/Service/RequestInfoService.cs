using MongoDB.Driver;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Models;

namespace RealStateMVCWebApp.Service
{
    public class RequestInfoService
    {
        private readonly IMongoCollection<RequestInfo> _collection;

        public RequestInfoService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(MongoDBConnection.DBName);
            _collection = database.GetCollection<RequestInfo>("request_info");
        }

        public async Task<List<RequestInfo>> Get()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<RequestInfo> Get(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(RequestInfo listing)
        {
            await _collection.InsertOneAsync(listing);
        }

        public async Task Update(RequestInfo listing)
        {
            await _collection.ReplaceOneAsync(p => p.Id == listing.Id, listing);
        }
        
    }
}
