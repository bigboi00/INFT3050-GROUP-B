using INFT3050.Models;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Employee")]
    public class AdminController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;

        public AdminController(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AuthUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    // Assign the "Employee" role to the user
                    var roleResult = await _userManager.AddToRoleAsync(user, "Employee");

                    if (roleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        // Delete the user if role assignment fails
                        await _userManager.DeleteAsync(user);

                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var getUser = await _userManager.GetUserAsync(User);

            if (getUser == null)
            {
                // Redirect to login or handle appropriately
                return RedirectToAction("Login", "Admin");
            }

            var users = _userManager.Users.ToList();
            var userVMs = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVMs.Add(new UserVM
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userVMs);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username!);
                    if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (user != null && await _userManager.IsInRoleAsync(user, "Employee"))
                    {
                        return RedirectToAction("Index", "Product");
                    }

                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "Only administrators and employees can sign in.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }
    }
}
