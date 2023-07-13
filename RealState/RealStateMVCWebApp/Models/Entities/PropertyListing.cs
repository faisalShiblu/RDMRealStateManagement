using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace RealStateMVCWebApp.Models.Entities
{
    [CollectionName("property_listing")]
    public class PropertyListing
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal YearlyTaxRate { get; set; }
        public decimal HomeOwnersAssociationFee { get; set; }
        public string BeforePriceLabel { get; set; } = string.Empty;
        public string AfterPriceLabel { get; set; } = string.Empty;
        public string PropertyStatus { get; set; } = string.Empty;

        public List<string> Images { get; set; } = new List<string>();
        public string VideoURLOne { get; set; } = string.Empty;
        public string VideoURLTwo { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();

        public decimal Size { get; set; }
        public decimal LotSize { get; set; }
        public int Rooms { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public string CustomID { get; set; } = string.Empty;
        public int Garages { get; set; }
        public decimal GarageSize { get; set; }
        public int YearBuilt { get; set; }

        public DateTime Availability { get; set; }
        public string Basement { get; set; } = string.Empty;
        public string Roofing { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string ExteriorMaterial { get; set; } = string.Empty;
        public string StructureType { get; set; } = string.Empty;
        public string FloorsNo { get; set; } = string.Empty;
        public string OwnerAgentNots { get; set; } = string.Empty;
        public string EnergyClass { get; set; } = string.Empty;
        public decimal EnergyIndex { get; set; }

        public string[] Amenities { get; set; } = Array.Empty<string>();
        public string[] Tags { get; set; } = Array.Empty<string>();

        public string PropertyCreatorId { get; set; } = string.Empty;

        public string AddedBy { get; set; } = string.Empty;
        public DateTime AddTimeStamp { get; set; }

        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdateTimeStamp { get; set; }

        public bool IsDeleted { get; set; } = false;
        public List<PropertyReview>? PropertyReviews { get; set; }
    }


}
