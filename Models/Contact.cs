using System.ComponentModel.DataAnnotations;

namespace PalsoftRealEstate.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [Required]
        [StringLength(100)]
        public string? Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

}
