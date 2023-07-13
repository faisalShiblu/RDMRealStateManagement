using MediatR;
using RealStateMVCWebApp.Commands;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Service;

namespace RealStateMVCWebApp.Handler
{
    public class UpdatePropertyListingCommandHandler: IRequestHandler<UpdatePropertyListingCommand, PropertyListing>
    {
        private readonly PropertyService _propertyRepository;

        public UpdatePropertyListingCommandHandler(PropertyService propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyListing> Handle(UpdatePropertyListingCommand request, CancellationToken cancellationToken)
        {
            var updatedPropertyListing = await _propertyRepository.Update(request.PropertyListing);
            return updatedPropertyListing;
        }
    }
}
