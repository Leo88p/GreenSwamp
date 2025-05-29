using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Lab3.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool IsPersistant { get; set; }

        private readonly SignInManager<Auth> _signInManager;
        private readonly UserManager<Auth> _userManager;
        private readonly SwampContext _dbContext;
        public LoginModel(UserManager<Auth> userManager, SignInManager<Auth> signInManager, SwampContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, Password, IsPersistant, false);

                if (result.Succeeded)
                    return Redirect("/Profile/" + User.Identity.Name);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
