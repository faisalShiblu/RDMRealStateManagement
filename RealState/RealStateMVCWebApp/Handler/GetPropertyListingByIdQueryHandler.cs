using MediatR;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Queries;
using RealStateMVCWebApp.Service;

namespace RealStateMVCWebApp.Handler
{
    public class GetPropertyListingByIdQueryHandler : IRequestHandler<GetPropertyListingByIdQuery, PropertyListing>
    {
        private readonly PropertyService _propertyRepository;

        public GetPropertyListingByIdQueryHandler(PropertyService propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyListing> Handle(GetPropertyListingByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyListing = await _propertyRepository.Get(request.Id);

            return propertyListing;
        }
    }
}
