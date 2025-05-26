using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace Lab3.Pages
{
    public class ProfileModel : PageModel
    {
        [FromRoute(Name = "username")]
        public string username { get; set; }
        public User user;
        public List<Post> posts;
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
                posts = await _context.Posts.Where(p => p.UserId == user.UserId).OrderByDescending(p => p.CreatedAt).ToListAsync();
                return Page();
            }
        }
        public static string TimeAgo(DateTime time)
        {
            TimeSpan difference = DateTime.Now - time;
            if (difference.TotalMinutes < 1)
            {
                return (int)difference.TotalSeconds + "s";
            }
            else if (difference.TotalHours < 1)
            {
                return (int)difference.TotalMinutes + "m";
            }
            else if (difference.TotalDays < 1)
            {
                return (int)difference.TotalHours + "h";
            }
            else
            {
                return (int)difference.TotalDays + "d";
            }
        }
    }
}
