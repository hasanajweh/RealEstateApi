using Microsoft.AspNetCore.Mvc;
using PalsoftRealEstate.Models;
using System.Text.Json;

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

            // Store services as comma-separated values
            property.ServicesString = string.Join(",", property.Services);

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
            existingProperty.ServicesString = string.Join(",", property.Services);

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

        [HttpPost("SubmitContactForm")]
        public IActionResult SubmitContactForm([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return Ok("Thank you for contacting us. We will get back to you shortly.");
        }

        // Admin Authentication
        [HttpPost("Admin/Login")]
        public IActionResult AdminLogin([FromBody] AdminLoginRequest request)
        {
            if (request.Email == "palrealstatea@gmail.com" && request.Password == "palestineAdminREstate1")
            {
                return Ok(new { Message = "Admin logged in successfully" });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        // Admin endpoint to view all contact messages
        [HttpGet("Admin/GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            var contacts = _context.Contacts.OrderByDescending(c => c.SubmittedAt).ToList();
            return Ok(contacts);
        }

        // Admin endpoint to get a specific contact message by ID
        [HttpGet("Admin/GetContact/{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
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
                CreatedAt = DateTime.Now,
                ServicesString = request.ServicesString // Store the services selected
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
