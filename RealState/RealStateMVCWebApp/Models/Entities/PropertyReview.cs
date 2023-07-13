using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealStateMVCWebApp.Models.Entities
{
    public class PropertyReview
    {
        public string Id { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public string PropertyListingId { get; set; } = string.Empty;
        public string ReviewerId { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
