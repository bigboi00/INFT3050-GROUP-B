using INFT3050.Data;
using INFT3050.Models;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INFT3050.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Product/Index
        // Retrieves and displays a list of all products, including their categories, ordered by product name
        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToList();
            return View(products);
        }

        // GET: /Product/Add
        // Displays the form to add a new product
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            return View("Edit", new Product());
        }

        // GET: /Product/Edit/{id}
        // Displays the form to edit an existing product identified by its ID
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            var product = _context.Products.Find(id);
            return View(product);
        }

        // POST: /Product/Edit
        // Handles the form submission for adding or editing a product
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                    _context.Products.Add(product); // Add new product
                else
                    _context.Products.Update(product); // Update existing product
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductId == 0) ? "Add" : "Edit";
                ViewBag.Categories = _context.Products.OrderBy(g => g.Name).ToList();
                return View(product);
            }
        }

        // GET: /Product/Delete/{id}
        // Displays the confirmation page for deleting a product identified by its ID
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

        // POST: /Product/Delete
        // Handles the form submission to delete a product
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        // GET: /Product/Details
        // Displays detailed information about a product based on room, category, and product name
        [HttpGet]
        public async Task<IActionResult> Details(string room, string category, string productName)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(room))
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Category.Room.ToLower() == room.ToLower() &&
                                          p.Category.Name.ToLower() == category.ToLower() &&
                                          p.Name.ToLower() == productName.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
