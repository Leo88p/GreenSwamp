using Lab3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lab3.Pages
{
    public class PondsModel : PageModel
    {
        private readonly SwampContext _context;
        public List<PostJoin> posts;
        [FromRoute(Name = "tag")]
        public string tag { get; set; }
        public PondsModel(SwampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            posts = await _context.Posts.Where(p => p.Content.Contains("#"+tag)).
                Join(_context.Users, p => p.UserId, u => u.UserId, (p, u) => new PostJoin(p, u)).ToListAsync();
            posts = posts.OrderByDescending(p => p.post.CreatedAt).ToList();
            return Page();
        }
    }
}
