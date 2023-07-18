using MediatR;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Commands
{
    public class UpdatePropertyListingCommand : IRequest<PropertyListing>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string PropertyType { get; set; }
        public decimal Price { get; set; }
        public decimal YearlyTaxRate { get; set; }
        public decimal HomeOwnersAssociationFee { get; set; }
        public string BeforePriceLabel { get; set; }
        public string AfterPriceLabel { get; set; }
        public string PropertyStatus { get; set; }

        public List<string> Images { get; set; }
        public string VideoURLOne { get; set; }
        public string VideoURLTwo { get; set; }

        public Address Address { get; set; }

        public decimal Size { get; set; }
        public decimal LotSize { get; set; }
        public int Rooms { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public string CustomID { get; set; }
        public int Garages { get; set; }
        public decimal GarageSize { get; set; }
        public int YearBuilt { get; set; }

        public DateTime Availability { get; set; }
        public string Basement { get; set; }
        public string Roofing { get; set; }
        public string ExtraDetails { get; set; }
        public string ExteriorMaterial { get; set; }
        public string StructureType { get; set; }
        public string FloorsNo { get; set; }
        public string OwnerAgentNots { get; set; }
        public string EnergyClass { get; set; }
        public decimal EnergyIndex { get; set; }

        public string[] Amenities { get; set; }
        public string[] Tags { get; set; }

        public string AddedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string PropertyCreatorId { get; set; }
        public string Id { get; set; } 
    }
}
