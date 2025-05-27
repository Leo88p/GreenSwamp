using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Pages.Feed
{
    public class PostModel : PageModel
    {
        [FromRoute(Name = "postId")]
        public long postId { get; set; }
        private readonly SwampContext _context;
        public PostJoin post;
        public List<PostJoin> children;
        public PostModel(SwampContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var posts = await _context.Posts.Where(p => p.PostId == postId).
                Join(_context.Users, p => p.UserId, u => u.UserId, (p, u) => new PostJoin(p, u)).ToListAsync();
            if (posts.Count == 0)
            {
                return RedirectToPage("/404");
            }
            else
            {
                post = posts[0];
                children = await _context.Posts.Where(p => p.ParentPostId == post.post.PostId).
                    Join(_context.Users, p => p.UserId, u => u.UserId, (p, u) => new PostJoin(p, u)).ToListAsync();
                //posts = await _context.Posts.Where(p => p.UserId == user.UserId).OrderByDescending(p => p.CreatedAt).ToListAsync();
                return Page();
            }
        }
    }
}
