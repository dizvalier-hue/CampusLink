using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusLink.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ListingId { get; set; }

        [ForeignKey("ListingId")]
        public Listing? Listing { get; set; }

        [Required]
        [DisplayName("Student Email")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@uc\.edu$", ErrorMessage = "Only verified @uc.edu students can book.")]
        public string BookerEmail { get; set; } = string.Empty;

        [Required]
        [DisplayName("Booking Date")]
        public DateTime BookingDate { get; set; } = DateTime.Now.AddDays(1); // Defaults to tomorrow

        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    }
}