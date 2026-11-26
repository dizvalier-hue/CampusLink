using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CampusLink.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}