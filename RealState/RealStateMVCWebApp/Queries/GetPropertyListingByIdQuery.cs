using MediatR;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Queries
{
    public class GetPropertyListingByIdQuery : IRequest<PropertyListing>
    {
        public string Id { get; set; }
    }
}
