using MediatR;

namespace RealStateMVCWebApp.Commands
{
    public class DeletePropertyListingCommand : IRequest
    {
        public string Id { get; set; }
    }
}
