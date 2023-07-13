using MongoDbGenericRepository.Attributes;

namespace RealStateMVCWebApp.Models.Entities
{
    [CollectionName("request_info")]
    public class RequestInfo
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyListingId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string RequestStatus { get; set; } = string.Empty;
        public string RequestDetails { get; set; } = string.Empty;
        public DateTime CreatedTimeStamp { get; set; }
    }
}
