using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Pages
{
    public class FeedModel : PageModel
    {
        private readonly SwampContext _context;
        public List<PostUser> posts;
        public FeedModel(SwampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            posts = await _context.Posts.OrderByDescending(p => p.CreatedAt).Join(_context.Users, p => p.UserId, u => u.UserId, (p,u) => new PostUser(p, u)).ToListAsync();
            return Page();
        }
    }
    public class PostUser
    {
        public Post post;
        public User? user;
        public PostUser(Post post, User user)
        {
            this.post = post;
            this.user = user;
        }
    }
}
