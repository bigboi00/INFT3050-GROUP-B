// ProductController class for handling product-related actions
// Index action for displaying a list of products
namespace INFT3050.Controllers
{
    public class ProductController : Controller
    {
        // Static list of sample products
        private static readonly List<Product> Products = new List<Product>
        {
            // Sample product objects with Id, Name, Price, Description, and ImageUrl properties initialized
            new Product { Id = 1, Name = "Product 1", Price = 19.99M, Description = "Description for product 1", ImageUrl = "/images/product1.jpg" },
            new Product { Id = 2, Name = "Product 2", Price = 29.99M, Description = "Description for product 2", ImageUrl = "/images/product2.jpg" },
            new Product { Id = 3, Name = "Product 3", Price = 39.99M, Description = "Description for product 3", ImageUrl = "/images/product3.jpg" },
        };

        // GET: Products
        // Action method for displaying a list of products
        public ActionResult Index()
        {
            return View(Products); // Returning the list of products to the view
        }
    }
}
