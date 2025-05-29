using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab3.Pages
{
    public class RegisterModel : PageModel
    {
        public string Result { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        private readonly SignInManager<Auth> _signInManager;
        private readonly UserManager<Auth> _userManager;
        private readonly SwampContext _dbContext;
        public RegisterModel(UserManager<Auth> userManager, SignInManager<Auth> signInManager, SwampContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Check if username or email already exists
            if (await _userManager.FindByNameAsync(Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return Page();
            }

            if (await _userManager.FindByEmailAsync(Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return Page();
            }

            

            // Now create the Auth entity with the same ID
            var authUser = new Auth
            {
                UserName = Username,
                Email = Email,
            };

            // Create the user in Identity
            var result = await _userManager.CreateAsync(authUser, Password);
            Result = result.ToString();
            if (result.Succeeded)
            {
                // First create the User entity
                var userEntity = new User
                {
                    //UserId = authUser.Id,
                    Username = Username,
                    DisplayName = Username,
                    Bio = "New user",
                    AvatarUrl = "https://i.pravatar.cc/100",
                };

                _dbContext.Users.Add(userEntity);
                await _dbContext.SaveChangesAsync(); // This generates the UserId

                await _signInManager.SignInAsync(authUser, isPersistent: true);

                return RedirectToPage("Profile/"+ User.Identity.Name);
            }

            // If Identity creation fails, clean up the User entity we created
            //_dbContext.Users.Remove(userEntity);
            await _dbContext.SaveChangesAsync();

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return Page();
        }
    }
}
