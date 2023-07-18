using MediatR;
using RealStateMVCWebApp.Commands;
using RealStateMVCWebApp.Models;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Service;
using Microsoft.AspNetCore.Identity;

namespace RealStateMVCWebApp.Handler
{
    public class CreatePropertyListingCommandHandler : IRequestHandler<CreatePropertyListingCommand, PropertyListing>
    {
        private readonly PropertyService _propertyRepository;

        public CreatePropertyListingCommandHandler(PropertyService propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyListing> Handle(CreatePropertyListingCommand request, CancellationToken cancellationToken)
        {
            var listing = new PropertyListing()
            {
                AddedBy = request.AddedBy,
                Address = request.Address,
                AddTimeStamp = DateTime.Now,
                AfterPriceLabel = request.AfterPriceLabel,
                Amenities = request.Amenities,
                Availability = request.Availability,
                Basement = request.Basement,
                BathRooms = request.BathRooms,
                BedRooms = request.BedRooms,
                BeforePriceLabel = request.BeforePriceLabel,
                Category = request.Category,
                CustomID = request.CustomID,
                Description = request.Description,
                EnergyClass = request.EnergyClass,
                EnergyIndex = request.EnergyIndex,
                ExteriorMaterial = request.ExteriorMaterial,
                ExtraDetails = request.ExtraDetails,
                FloorsNo = request.FloorsNo,
                Garages = request.Garages,
                GarageSize = request.GarageSize,
                HomeOwnersAssociationFee = request.HomeOwnersAssociationFee,
                Images = request.Images,
                IsDeleted = false,
                LotSize = request.LotSize,
                OwnerAgentNots = request.OwnerAgentNots,
                Price = request.Price,
                PropertyCreatorId = request.PropertyCreatorId,
                PropertyReviews = null,
                PropertyStatus = request.PropertyStatus,
                PropertyType = request.PropertyType,
                Roofing = request.Roofing,
                Rooms = request.Rooms,
                Size = request.Size,
                StructureType = request.StructureType,
                Tags = request.Tags,
                Title = request.Title,
                UpdatedBy = request.UpdatedBy,
                UpdateTimeStamp = DateTime.Now,
                VideoURLOne = request.VideoURLOne,
                VideoURLTwo = request.VideoURLTwo,
                YearBuilt = request.YearBuilt,
                YearlyTaxRate = request.YearlyTaxRate,
                Id = Guid.NewGuid().ToString()
            };

            var createdPropertyListing = await _propertyRepository.Create(listing);

            return createdPropertyListing;
        }
    }
}
