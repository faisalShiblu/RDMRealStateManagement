using MediatR;
using RealStateMVCWebApp.Models.Entities;

namespace RealStateMVCWebApp.Commands
{
    public class UpdatePropertyListingCommand : IRequest<PropertyListing>
    {
        public PropertyListing PropertyListing { get; set; }
    }
}
