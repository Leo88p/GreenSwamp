using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Lab3.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Pages
{
    public class ProfileModel : PageModel
    {
        [FromRoute(Name = "username")]
        public string username { get; set; }
        public User user;
        private readonly SwampContext _context;
        public ProfileModel(SwampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _context.Users.Where(u => u.Username == username).ToListAsync();
            if (users.Count == 0)
            {
                return RedirectToPage("/404");
            }
            else
            {
                user = users[0];
                return Page();
            }
        }
    }
}
