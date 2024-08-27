﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PalsoftRealEstate.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? City { get; set; }

        [Required]
        [StringLength(150)]
        public string? Address { get; set; }

        [Required]
        [StringLength(50)]
        public string? PropertyType { get; set; } // Example values: "Apartment", "House", "Land"

        [Required]
        [StringLength(50)]
        public string? OfferType { get; set; } // Example values: "For Sale", "For Rent"

        [Range(0, double.MaxValue)]
        public double? Price { get; set; }

        [Range(0, double.MaxValue)]
        public double? Space { get; set; } // The area of the property

        [StringLength(20)]
        public string? MeasurementUnit { get; set; } // Example values: "Square meters", "Acres"

        public bool IsApproved { get; set; } = false; // Approval status of the property
        public DateTime CreatedAt { get; set; } = DateTime.Now; // The date and time the property was created

        
        public ICollection<AdvertiseRequest> AdvertiseRequests { get; set; } = new List<AdvertiseRequest>();
    }
}
