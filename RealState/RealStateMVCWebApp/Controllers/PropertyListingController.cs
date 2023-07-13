using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealStateMVCWebApp.Commands;
using RealStateMVCWebApp.DTO.PropertyListing;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Queries;

namespace RealStateMVCWebApp.Controllers
{
    public class PropertyListingController : Controller
    {
        private readonly IMediator _mediator;
        public PropertyListingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetPropertyListingsQuery());
            var propertyListingDTOs = response.Select(pl => new PropertyListingDTO
            {
                Id = pl.Id,
                Title = pl.Title,
                Description = pl.Description,
                PropertyStatus = pl.PropertyStatus,
                Address = pl.Address,
                PropertyType = pl.PropertyType,
                Price = pl.Price
            }).ToList();
            return View(propertyListingDTOs);
        }

        public IActionResult Create()
        {
            ViewBag.Category = new List<string> { "Rent", "Commercial", "Industrial" };
            ViewBag.PropertyType = new List<string> { "Rent", "Commercial", "Industrial" };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyListingDTO dto)
        {

            var response = await _mediator.Send(new CreatePropertyListingCommand() { });

            return View();
        }

    }
}
