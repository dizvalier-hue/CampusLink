using System.ComponentModel.DataAnnotations;

namespace CampusLink.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "School Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@uc\.edu$", ErrorMessage = "You must use a valid @uc.edu email.")] public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}