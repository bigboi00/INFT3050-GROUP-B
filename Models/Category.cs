namespace INFT3050.Models
{
    // Represents a category within the system
    public class Category
    {
        // Unique identifier for the category
        public string CategoryId { get; set; } = string.Empty;
        
        // Name of the category
        public string Name { get; set; } = string.Empty;    

        // Room associated with the category
        public string Room { get; set; } 
    }
}
