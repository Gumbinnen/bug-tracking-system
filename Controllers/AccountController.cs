using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDBContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (isPasswordValid)
            {
                /// Password is correct, sign in
                var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Project");
                }
            }
            /// Password is incorrect
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }
    }
}
