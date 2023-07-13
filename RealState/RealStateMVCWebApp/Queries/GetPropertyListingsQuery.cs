using MediatR;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Queries
{
    public class GetPropertyListingsQuery: IRequest<List<PropertyListing>>
    {
    }
}
