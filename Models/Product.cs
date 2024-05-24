// Product class with properties for Id, Name, Price, Description, and ImageUrl
// Annotating Name and Price properties with Required attribute
// Annotating Name property with StringLength attribute
namespace INFT3050.Models
{
    public class Product
    {
        public int Id { get; set; } // Id property

        [Required] // Name property is required
        [StringLength(100)] // Name property should have a maximum length of 100 characters
        public string Name { get; set; }

        [Required] // Price property is required
        public decimal Price { get; set; }

        [StringLength(500)] // Description property should have a maximum length of 500 characters
        public string Description { get; set; }

        [StringLength(100)] // ImageUrl property should have a maximum length of 100 characters
        public string ImageUrl { get; set; }
    }
}
