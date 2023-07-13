using MediatR;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Queries;
using RealStateMVCWebApp.Service;

namespace RealStateMVCWebApp.Handler
{
    public class GetPropertyListingsQueryHandler : IRequestHandler<GetPropertyListingsQuery, List<PropertyListing>>
    {
        private readonly PropertyService _propertyRepository;

        public GetPropertyListingsQueryHandler(PropertyService propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyListing>> Handle(GetPropertyListingsQuery request, CancellationToken cancellationToken)
        {
            var propertyListings = await _propertyRepository.Get();
            return propertyListings.Where(p => p.IsDeleted == false).ToList();
        }
    }
}
