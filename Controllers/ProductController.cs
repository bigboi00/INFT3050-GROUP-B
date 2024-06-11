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

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Include(p => p.Category    )
               .OrderBy(p => p.Name)
               .ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            return View("Edit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0)
                    _context.Products.Add(product);
                else
                    _context.Products.Update(product);
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

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

