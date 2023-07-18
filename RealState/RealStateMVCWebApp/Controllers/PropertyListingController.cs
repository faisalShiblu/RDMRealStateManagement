using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealStateMVCWebApp.Commands;
using RealStateMVCWebApp.DTO.Account;
using RealStateMVCWebApp.DTO.PropertyListing;
using RealStateMVCWebApp.Models;
using RealStateMVCWebApp.Models.Entities;
using RealStateMVCWebApp.Queries;
using System.Drawing;
using System.Xml.Linq;


namespace RealStateMVCWebApp.Controllers
{
    public class PropertyListingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreatePropertyListingDTO> _createValidator;
        private readonly IValidator<EditPropertyListingDTO> _editValidator;
        public PropertyListingController(IMediator mediator, IValidator<CreatePropertyListingDTO> createValidator, IValidator<EditPropertyListingDTO> editValidator)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _editValidator = editValidator;
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
                Price = pl.Price,
                Category = pl.Category,
            }).ToList();
            return View(propertyListingDTOs);
        }

        public IActionResult Create()
        {
            ViewBag.Category = new List<string> { "Premium", "Commercial", "Industrial" };
            ViewBag.PropertyType = new List<string> { "Single-Family Home", "Bungalow", "Others" };
            ViewBag.PropertyStatus = new List<string> { "Available", "On Hold", "Sold" };
            ViewBag.Country = new List<string> { "Bangladesh", "Others" };
            ViewBag.StructureType = new List<string> { "A", "B", "C" };
            ViewBag.FloorsNo = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ViewBag.EnergyClass = new List<string> { "A++", "A+", "A", "B", "C", "D" };
            ViewBag.EnergyIndex = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ViewBag.AmenitiesList = new List<string> { "Attic", "Basketball", "court", "Doorman", "Front yard", "Lake view", "Ocean view", "Private space", "Sprinklers", "Wine cellar" };


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyListingDTO dto)
        {
            try
            {
                ViewBag.Category = new List<string> { "Rent", "Commercial", "Industrial" };
                ViewBag.PropertyType = new List<string> { "Single-Family Home", "Bungalow", "Others" };
                ViewBag.PropertyStatus = new List<string> { "Available", "On Hold", "Sold" };
                ViewBag.Country = new List<string> { "Bangladesh", "Others" };
                ViewBag.StructureType = new List<string> { "A", "B", "C" };
                ViewBag.FloorsNo = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.EnergyClass = new List<string> { "A++", "A+", "A", "B", "C", "D" };
                ViewBag.EnergyIndex = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.AmenitiesList = new List<string> { "Attic", "Basketball", "court", "Doorman", "Front yard", "Lake view", "Ocean view", "Private space", "Sprinklers", "Wine cellar" };

                var validatorResult = _createValidator.Validate(dto);

                if (!validatorResult.IsValid)
                {
                    foreach (var error in validatorResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(dto);
                }

                var location = new Location
                {
                    IsLocationExact = false,
                    LanCoordinate = dto.LanCoordinate,
                    LonCoordinate = dto.LanCoordinate,
                    Type = "Point"
                };
                var address = new Address
                {
                    City = dto.City,
                    Country = dto.Country,
                    State = dto.State,
                    DetailedAddress = dto.DetailedAddress,
                    Location = location,
                    Neighborhood = dto.Neighborhood,
                    Zip = dto.Zip
                };
                string[] amenities = new string[0];
                if (!string.IsNullOrWhiteSpace(dto.AmenitiesStr))
                    amenities = dto.AmenitiesStr.Split(',');


                var response = await _mediator.Send(new CreatePropertyListingCommand()
                {
                    AddedBy = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    PropertyType = dto.PropertyType,
                    Price = dto.Price,
                    PropertyStatus = dto.PropertyStatus,
                    AfterPriceLabel = dto.AfterPriceLabel,
                    Amenities = amenities,
                    Availability = dto.Availability,
                    Basement = dto.Basement,
                    BathRooms = dto.BathRooms,
                    BedRooms = dto.BedRooms,
                    BeforePriceLabel = dto.BeforePriceLabel,
                    Category = dto.Category,
                    CustomID = dto.CustomID,
                    Description = dto.Description,
                    EnergyClass = dto.EnergyClass,
                    EnergyIndex = dto.EnergyIndex,
                    ExteriorMaterial = dto.ExteriorMaterial,
                    ExtraDetails = dto.ExtraDetails,
                    FloorsNo = dto.FloorsNo,
                    Garages = dto.Garages,
                    GarageSize = dto.GarageSize,
                    HomeOwnersAssociationFee = dto.HomeOwnersAssociationFee,
                    Images = dto.Images,
                    LotSize = dto.LotSize,
                    OwnerAgentNots = dto.OwnerAgentNots,
                    PropertyCreatorId = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    Roofing = dto.Roofing,
                    Rooms = dto.Rooms,
                    Size = dto.Size,
                    StructureType = dto.StructureType,
                    Tags = Array.Empty<string>(),
                    Title = dto.Title,
                    UpdatedBy = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    VideoURLOne = dto.VideoURLOne,
                    VideoURLTwo = dto.VideoURLTwo,
                    YearBuilt = dto.YearBuilt,
                    YearlyTaxRate = dto.YearlyTaxRate,
                    Address = address
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var reponse = await _mediator.Send(new GetPropertyListingByIdQuery() { Id = id });
            if (reponse.Id == id)
            {
                ViewBag.Category = new List<string> { "Premium", "Commercial", "Industrial" };
                ViewBag.PropertyType = new List<string> { "Single-Family Home", "Bungalow", "Others" };
                ViewBag.PropertyStatus = new List<string> { "Available", "On Hold", "Sold" };
                ViewBag.Country = new List<string> { "Bangladesh", "Others" };
                ViewBag.StructureType = new List<string> { "A", "B", "C" };
                ViewBag.FloorsNo = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.EnergyClass = new List<string> { "A++", "A+", "A", "B", "C", "D" };
                ViewBag.EnergyIndex = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.AmenitiesList = new List<string> { "Attic", "Basketball", "court", "Doorman", "Front yard", "Lake view", "Ocean view", "Private space", "Sprinklers", "Wine cellar" };
                var dto = new EditPropertyListingDTO
                {
                    AfterPriceLabel = reponse.AfterPriceLabel,
                    Id = reponse.Id,
                    Availability = reponse.Availability,
                    Basement = reponse.Basement,
                    BathRooms = reponse.BathRooms,
                    BedRooms = reponse.BedRooms,
                    BeforePriceLabel = reponse.BeforePriceLabel,
                    Category = reponse.Category,
                    City = reponse.Address.City,
                    Country = reponse.Address.Country,
                    CustomID = reponse.CustomID,
                    Description = reponse.Description,
                    DetailedAddress = reponse.Address.DetailedAddress,
                    EnergyClass = reponse.EnergyClass,
                    EnergyIndex = reponse.EnergyIndex,
                    ExteriorMaterial = reponse.ExteriorMaterial,
                    ExtraDetails = reponse.ExtraDetails,
                    FloorsNo = reponse.FloorsNo,
                    Garages = reponse.Garages,
                    GarageSize = reponse.GarageSize,
                    HomeOwnersAssociationFee = reponse.HomeOwnersAssociationFee,
                    IsLocationExact = reponse.Address.Location.IsLocationExact,
                    Images = reponse.Images,
                    LanCoordinate = reponse.Address.Location.LanCoordinate,
                    LonCoordinate = reponse.Address.Location.LonCoordinate,
                    LotSize = reponse.LotSize,
                    Neighborhood = reponse.Address.Neighborhood,
                    OwnerAgentNots = reponse.OwnerAgentNots,
                    Price = reponse.Price,
                    PropertyStatus = reponse.PropertyStatus,
                    PropertyType = reponse.PropertyType,
                    Roofing = reponse.Roofing,
                    Rooms = reponse.Rooms,
                    Size = reponse.Size,
                    State = reponse.Address.State,
                    StructureType = reponse.StructureType,
                    Tags = reponse.Tags,
                    Title = reponse.Title,
                    VideoURLOne = reponse.VideoURLOne,
                    Type = reponse.Address.Location.Type,
                    VideoURLTwo = reponse.VideoURLTwo,
                    YearBuilt = reponse.YearBuilt,
                    YearlyTaxRate = reponse.YearlyTaxRate,
                    Zip = reponse.Address.Zip,
                    Amenities = string.Empty
                };

                ViewBag.AmenitiesCheckedList = reponse.Amenities.ToList();

                return View(dto);
            }
            else
            {
                return NotFound();
            }



        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPropertyListingDTO dto)
        {
            try
            {
                ViewBag.Category = new List<string> { "Rent", "Commercial", "Industrial" };
                ViewBag.PropertyType = new List<string> { "Single-Family Home", "Bungalow", "Others" };
                ViewBag.PropertyStatus = new List<string> { "Available", "On Hold", "Sold" };
                ViewBag.Country = new List<string> { "Bangladesh", "Others" };
                ViewBag.StructureType = new List<string> { "A", "B", "C" };
                ViewBag.FloorsNo = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.EnergyClass = new List<string> { "A++", "A+", "A", "B", "C", "D" };
                ViewBag.EnergyIndex = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                ViewBag.AmenitiesList = new List<string> { "Attic", "Basketball", "court", "Doorman", "Front yard", "Lake view", "Ocean view", "Private space", "Sprinklers", "Wine cellar" };

                var validatorResult = _editValidator.Validate(dto);

                if (!validatorResult.IsValid)
                {
                    foreach (var error in validatorResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(dto);
                }

                var location = new Location
                {
                    IsLocationExact = false,
                    LanCoordinate = dto.LanCoordinate,
                    LonCoordinate = dto.LanCoordinate,
                    Type = "Point"
                };
                var address = new Address
                {
                    City = dto.City,
                    Country = dto.Country,
                    State = dto.State,
                    DetailedAddress = dto.DetailedAddress,
                    Location = location,
                    Neighborhood = dto.Neighborhood,
                    Zip = dto.Zip
                };
                string[] amenities = new string[0];
                if (!string.IsNullOrWhiteSpace(dto.AmenitiesStr))
                    amenities = dto.AmenitiesStr.Split(',');


                var response = await _mediator.Send(new UpdatePropertyListingCommand()
                {
                    AddedBy = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    PropertyType = dto.PropertyType,
                    Price = dto.Price,
                    PropertyStatus = dto.PropertyStatus,
                    AfterPriceLabel = dto.AfterPriceLabel,
                    Amenities = amenities,
                    Availability = dto.Availability,
                    Basement = dto.Basement,
                    BathRooms = dto.BathRooms,
                    BedRooms = dto.BedRooms,
                    BeforePriceLabel = dto.BeforePriceLabel,
                    Category = dto.Category,
                    CustomID = dto.CustomID,
                    Description = dto.Description,
                    EnergyClass = dto.EnergyClass,
                    EnergyIndex = dto.EnergyIndex,
                    ExteriorMaterial = dto.ExteriorMaterial,
                    ExtraDetails = dto.ExtraDetails,
                    FloorsNo = dto.FloorsNo,
                    Garages = dto.Garages,
                    GarageSize = dto.GarageSize,
                    HomeOwnersAssociationFee = dto.HomeOwnersAssociationFee,
                    Images = dto.Images,
                    LotSize = dto.LotSize,
                    OwnerAgentNots = dto.OwnerAgentNots,
                    PropertyCreatorId = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    Roofing = dto.Roofing,
                    Rooms = dto.Rooms,
                    Size = dto.Size,
                    StructureType = dto.StructureType,
                    Tags = Array.Empty<string>(),
                    Title = dto.Title,
                    UpdatedBy = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "",
                    VideoURLOne = dto.VideoURLOne,
                    VideoURLTwo = dto.VideoURLTwo,
                    YearBuilt = dto.YearBuilt,
                    YearlyTaxRate = dto.YearlyTaxRate,
                    Address = address,
                    Id = dto.Id,
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var reponse = await _mediator.Send(new GetPropertyListingByIdQuery() { Id = id });
            if (reponse.Id == id)
            {
                await _mediator.Send(new DeletePropertyListingCommand() { Id = id});
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }



        }
    }
}
