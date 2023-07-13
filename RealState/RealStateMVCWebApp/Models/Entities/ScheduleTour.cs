using MongoDbGenericRepository.Attributes;

namespace RealStateMVCWebApp.Models.Entities
{
    [CollectionName("schedule_tour")]
    public class ScheduleTour
    {
        public string Id { get; set; } = string.Empty;
        public string PropertyListingId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public DateTime ScheduleTimeStamp { get; set; }
        public string ScheduleStatus { get; set; } = string.Empty;
        public DateTime CreatedTimeStamp { get; set; }
    }
}
