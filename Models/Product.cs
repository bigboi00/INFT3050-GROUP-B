using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace INFT3050.Models
{
    // Represents a product within the system
    public class Product
    {
        // Unique identifier for the product
        public int ProductId { get; set; }

        // Name of the product, required field with a custom error message
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        // Price of the product, required field with a custom error message
        // Value must be between 1 and 10000
        [Required(ErrorMessage = "Please enter price number.")] 
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
        public decimal Price { get; set; }

        // Stock quantity of the product, required field with a custom error message
        // Value must be between 1 and 10000
        [Required(ErrorMessage = "Please enter how many stock.")]
        [Range(1, 10000, ErrorMessage = "Stock must be between 1 and 10000.")]
        public int Stock { get; set; }

        // Identifier for the category the product belongs to, required field with a custom error message
        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; } = string.Empty;

        // Navigation property for the category, not validated by the model binder
        [ValidateNever]
        public Category Category { get; set; } = null!;
    }
}
