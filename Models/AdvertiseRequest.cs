using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalsoftRealEstate.Models
{
    public class AdvertiseRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [StringLength(100)]
        public string? AdvertiserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string? City { get; set; }

        [Required]
        [StringLength(50)]
        public string? PropertyType { get; set; }

        [Required]
        [StringLength(50)]
        public string? OfferType { get; set; }

        [Range(0, double.MaxValue)]
        public double? Price { get; set; }

        [Range(0, double.MaxValue)]
        public double? Space { get; set; }

        [StringLength(20)]
        public string? MeasurementUnit { get; set; }

        [Required]
        [StringLength(150)]
        public string? Address { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsApproved { get; set; } = false;

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        // stores the list of services selected by the advertiser
        [NotMapped]
        public List<string>? Services { get; set; }

        public string ServicesString
        {
            get => Services != null ? string.Join(",", Services) : string.Empty;
            set => Services = value.Split(',').ToList();
        }

       

        // Foreign Key for Property (if approved and converted)
        public int? PropertyId { get; set; }
        public Property? Property { get; set; }
    }

}
