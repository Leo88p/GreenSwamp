/*using Lab3.Data;
using Lab3.Models;
using Lab3.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Lab3.Controllers
{

    [Route("register")]
    public class AccountController : Controller
    {
        private readonly SignInManager<Auth> _signInManager;
        private readonly UserManager<Auth> _userManager;
        private readonly SwampContext _dbContext;
        public AccountController(UserManager<Auth> userManager, SignInManager<Auth> signInManager, SwampContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        /*[HttpGet]
        public IActionResult Login(string returnUrl = "/") => View(new LoginModel { ReturnUrl = returnUrl });
        */
        /*[HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // if (!ModelState.IsValid) return View(model);

            //  var user = await _userManager.FindByNameAsync(model.Username);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        } */
        /*
        [HttpGet]
        public IActionResult Register(string returnUrl = "/") => View(new RegisterModel { ReturnUrl = returnUrl });
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if username or email already exists
            if (await _userManager.FindByNameAsync(model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            // First create the User entity
            var userEntity = new User
            {
                Username = model.Username,
                DisplayName = model.Username,
                Bio = "New user",
                AvatarUrl = "https://i.pravatar.cc/100",
            };

            _dbContext.Users.Add(userEntity);
            await _dbContext.SaveChangesAsync(); // This generates the UserId

            // Now create the Auth entity with the same ID
            var authUser = new Auth
            {
                Id = userEntity.UserId, // Explicitly set the ID to match User.UserId
                UserName = model.Username,
                Email = model.Email,
            };

            // Create the user in Identity
            var result = await _userManager.CreateAsync(authUser, model.Password);
            model.Result = result.ToString();
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(authUser, isPersistent: true);

                return RedirectToPage("Feed");
            }

            // If Identity creation fails, clean up the User entity we created
            _dbContext.Users.Remove(userEntity);
            await _dbContext.SaveChangesAsync();

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        /*
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}*/
