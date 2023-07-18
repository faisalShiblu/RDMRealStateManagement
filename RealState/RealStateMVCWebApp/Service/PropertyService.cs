using MongoDB.Bson;
using MongoDB.Driver;
using RealStateMVCWebApp.Models;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Service
{
    public class PropertyService
    {
        private readonly IMongoCollection<PropertyListing> _collection;

        public PropertyService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(MongoDBConnection.DBName);
            _collection = database.GetCollection<PropertyListing>("property_listing");
        }

        public async Task<List<PropertyListing>> Get()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<PropertyListing> Get(string id)
        {
            return await _collection.Find(p => p.Id.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task<PropertyListing> Create(PropertyListing listing)
        {
            await _collection.InsertOneAsync(listing);
            return listing;
        }

        public async Task<PropertyListing> Update(PropertyListing listing)
        {
            try
            {
                await _collection.ReplaceOneAsync(p => p.Id == listing.Id.Trim(), listing);
                return listing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task Delete(string id)
        {
            var update = Builders<PropertyListing>.Update.Set(s => s.IsDeleted, true);
            await _collection.UpdateOneAsync(p => p.Id == id.Trim(), update);

        }

    }
}
