using Microsoft.AspNetCore.Mvc; 
using INFT3050.ViewModel;
using INFT3050.Data;
using Microsoft.EntityFrameworkCore;
using INFT3050.Models;

namespace INFT3050.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FurnitureController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LivingRoom(string categoryId)
        {
            if (categoryId == "LivingRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Living-Room");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Living Room";

                var products = await _context.Products
                                             .Where(p => p.Category.Room == "Living-Room")
                                             .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;

                var products = await _context.Products
                                            .Where(p => p.CategoryId == categoryId)
                                            .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }

        public async Task<IActionResult> DiningRoom(string categoryId)
        {
            if (categoryId == "DiningRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Dining-Room");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Dining Room";

                var products = await _context.Products
                                             .Where(p => p.Category.Room == "Dining-Room")
                                             .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;

                var products = await _context.Products
                                            .Where(p => p.CategoryId == categoryId)
                                            .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }

        public async Task<IActionResult> BedRoom(string categoryId)
        {
            if (categoryId == "BedRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Beds");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Bed Room";

                var products = await _context.Products
                                            .Where(p => p.Category.Room == "Beds")
                                            .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;

                var products = await _context.Products
                                            .Where(p => p.CategoryId == categoryId)
                                            .ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }


        public IActionResult Product(string name)
        {
            // Find product by Name (assuming it's unique)
            var product = _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
