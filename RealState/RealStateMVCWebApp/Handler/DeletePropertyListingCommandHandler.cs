using MediatR;
using RealStateMVCWebApp.Commands;
using RealStateMVCWebApp.Service;

namespace RealStateMVCWebApp.Handler
{
    public class DeletePropertyListingCommandHandler : IRequestHandler<DeletePropertyListingCommand>
    {
        private readonly PropertyService _propertyRepository;

        public DeletePropertyListingCommandHandler(PropertyService propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task Handle(DeletePropertyListingCommand request, CancellationToken cancellationToken)
        {
            await _propertyRepository.Delete(request.Id);
        }       

    }
}