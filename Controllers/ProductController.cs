// ProductController class for handling product-related actions
// Index action for displaying a list of products
using INFT3050.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class ProductController : Controller
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 19.99M, Description = "Description for product 1", ImageUrl = "/images/product1.jpg" },
            new Product { Id = 2, Name = "Product 2", Price = 29.99M, Description = "Description for product 2", ImageUrl = "/images/product2.jpg" },
            new Product { Id = 3, Name = "Product 3", Price = 39.99M, Description = "Description for product 3", ImageUrl = "/images/product3.jpg" },
        };

        // GET: Products
        public ActionResult Index()
        {
            return View(Products);
        }
    }
}
