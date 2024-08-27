using Microsoft.AspNetCore.Mvc;
using PalsoftRealEstate.Models;

namespace PalsoftRealEstate.Controllers
{
    [Route("api/Property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertyController(AppDbContext context)
        {
            _context = context;
        }

        // Regular User Endpoints

        [HttpGet("GetAllProperties")]
        public IActionResult GetAllProperties()
        {
            var properties = _context.Properties.Where(p => p.IsApproved).ToList();
            return Ok(properties);
        }

        [HttpGet("GetProperty/{id}")]
        public IActionResult GetProperty(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == id && p.IsApproved);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        [HttpPost("AddProperty")]
        public IActionResult AddProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Properties.Add(property);
            _context.SaveChanges();
            return Ok("Property added successfully");
        }

        [HttpPut("UpdateProperty/{id}")]
        public IActionResult UpdateProperty(int id, [FromBody] Property property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProperty = _context.Properties.FirstOrDefault(p => p.PropertyId == id);
            if (existingProperty == null)
                return NotFound();

            existingProperty.Name = property.Name;
            existingProperty.Description = property.Description;
            existingProperty.Price = property.Price;
            existingProperty.Space = property.Space;

            _context.SaveChanges();
            return Ok("Property updated successfully");
        }

        [HttpDelete("DeleteProperty/{id}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == id);
            if (property == null)
                return NotFound();

            _context.Properties.Remove(property);
            _context.SaveChanges();
            return Ok("Property deleted successfully");
        }

        // Admin Endpoints

        [HttpGet("Admin/GetAdvertiseRequests")]
        public IActionResult GetAdvertiseRequests()
        {
            var requests = _context.AdvertiseRequests.Where(r => !r.IsApproved).ToList();
            return Ok(requests);
        }

        [HttpPut("Admin/ApproveRequest/{id}")]
        public IActionResult ApproveRequest(int id)
        {
            var request = _context.AdvertiseRequests.FirstOrDefault(r => r.RequestId == id);
            if (request == null)
                return NotFound();

            request.IsApproved = true;
            _context.SaveChanges();

            var property = new Property
            {
                Name = request.AdvertiserName,
                City = request.City,
                Address = request.Address,
                PropertyType = request.PropertyType,
                OfferType = request.OfferType,
                Price = request.Price,
                Space = request.Space,
                MeasurementUnit = request.MeasurementUnit,
                IsApproved = true,
                CreatedAt = DateTime.Now
            };
            _context.Properties.Add(property);
            _context.SaveChanges();

            return Ok("Request approved");
        }

        [HttpPut("Admin/RejectRequest/{id}")]
        public IActionResult RejectRequest(int id)
        {
            var request = _context.AdvertiseRequests.FirstOrDefault(r => r.RequestId == id);
            if (request == null)
                return NotFound();

            _context.AdvertiseRequests.Remove(request);
            _context.SaveChanges();

            return Ok("Request rejected");
        }
    }
}
