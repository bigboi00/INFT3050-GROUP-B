//In Progress
using System.ComponentModel.DataAnnotations;

namespace INFT3050.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }
    }
}
