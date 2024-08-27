using System.ComponentModel.DataAnnotations;

namespace PalsoftRealEstate.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
    }

}
