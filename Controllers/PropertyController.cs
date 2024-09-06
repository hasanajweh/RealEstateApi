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
        // Get all approved properties
        [HttpGet("GetAllProperties")]
        public IActionResult GetAllProperties()
        {
            var properties = _context.Properties.Where(p => p.IsApproved).ToList();
            return Ok(properties);
        }

        // Get a specific approved property by ID
        [HttpGet("GetProperty/{id}")]
        public IActionResult GetProperty(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == id && p.IsApproved);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        // Submit an advertisement request (replaces AddProperty for users)
        [HttpPost("SubmitAdvertiseRequest")]
        public IActionResult SubmitAdvertiseRequest([FromBody] AdvertiseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Save the advertise request for admin approval later
            _context.AdvertiseRequests.Add(request);
            _context.SaveChanges();
            return Ok("Advertise request submitted. Awaiting admin approval.");
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
        // Get all pending advertise requests
        [HttpGet("Admin/GetAdvertiseRequests")]
        public IActionResult GetAdvertiseRequests()
        {
            var requests = _context.AdvertiseRequests.Where(r => !r.IsApproved).ToList();
            return Ok(requests);
        }

        // Admin approves the advertise request and converts it to a property
        [HttpPut("Admin/ApproveRequest/{id}")]
        public IActionResult ApproveRequest(int id)
        {
            var request = _context.AdvertiseRequests.FirstOrDefault(r => r.RequestId == id);
            if (request == null)
                return NotFound();

            // Mark the advertise request as approved
            request.IsApproved = true;
            _context.SaveChanges();

            // Convert the request to a property
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
                ServicesString = request.ServicesString 
            };

            _context.Properties.Add(property);
            _context.SaveChanges();

            return Ok("Request approved and property added.");
        }

        // Admin rejects an advertisement request
        [HttpPut("Admin/RejectRequest/{id}")]
        public IActionResult RejectRequest(int id)
        {
            var request = _context.AdvertiseRequests.FirstOrDefault(r => r.RequestId == id);
            if (request == null)
                return NotFound();

            _context.AdvertiseRequests.Remove(request);
            _context.SaveChanges();

            return Ok("Request rejected.");
        }

        // Contact API 
        [HttpPost("SubmitContactForm")]
        public IActionResult SubmitContactForm([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return Ok("Thank you for contacting us. We will get back to you shortly.");
        }
        [HttpDelete("Admin/DeleteProperty/{id}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyId == id);
            if (property == null)
                return NotFound("Property not found");

            _context.Properties.Remove(property);
            _context.SaveChanges();

            return Ok("Property deleted successfully.");
        }
    }
}

