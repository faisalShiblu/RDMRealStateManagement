using MediatR;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Commands
{
    public class CreatePropertyListingCommand : IRequest<PropertyListing>
    {
        //public PropertyListing PropertyListing { get; set; }
        public CreatePropertyListingCommand()
        {
            Title = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            PropertyType = string.Empty;
            Price = 0;
            YearlyTaxRate = 0;
            HomeOwnersAssociationFee = 0;
            BeforePriceLabel = string.Empty;
            AfterPriceLabel = string.Empty;
            PropertyStatus = string.Empty;
            Images = new List<string>();
            VideoURLOne = string.Empty;
            VideoURLTwo = string.Empty;
            Address = new Address();
            Size = 0;
            LotSize = 0;
            Rooms = 0;
            BedRooms = 0;
            BathRooms = 0;
            CustomID = string.Empty;
            Garages = 0;
            GarageSize = 0;
            YearBuilt = 0;
            Availability = DateTime.MinValue;
            Basement = string.Empty;
            Roofing = string.Empty;
            ExtraDetails = string.Empty;
            ExteriorMaterial = string.Empty;
            StructureType = string.Empty;
            FloorsNo = string.Empty;
            OwnerAgentNots = string.Empty;
            EnergyClass = string.Empty;
            EnergyIndex = 0;
            Amenities = Array.Empty<string>();
            Tags = Array.Empty<string>();
            AddedBy = string.Empty;
            UpdatedBy = string.Empty;
            PropertyCreatorId = string.Empty;
        }
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
    }
}
