using CampusLink.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusLink.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Listing Type")]
        public ListingType Type { get; set; }

        // Category relationship
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        // Verified student email (must end with school domain)
        [Required]
        [EmailAddress]
        [DisplayName("Poster Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@uc\.edu$", ErrorMessage = "Only verified @uc.edu students can post.")]
        public string PosterEmail { get; set; } = string.Empty;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}