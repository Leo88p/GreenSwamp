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
        public List<PostJoin> posts;
        public FeedModel(SwampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            posts = await _context.Posts.Where(p=>p.PostType!="event").
                Join(_context.Users, p => p.UserId, u => u.UserId, (p,u) => new PostJoin(p, u)).ToListAsync();
            var events = await _context.Posts.Where(p => p.PostType == "event").
                Join(_context.Events, p => p.PostId, e => e.PostId, (p, e) => new PostJoin(p, e)).ToListAsync();
            posts.AddRange(events);
            posts = posts.OrderByDescending(p => p.post.CreatedAt).ToList();
            return Page();
        }
    }
    public class PostJoin
    {
        public Post post;
        public User? user;
        public Event? Event;
        public PostJoin(Post post, User user)
        {
            this.post = post;
            this.user = user;
        }
        public PostJoin(Post post, Event Event)
        {
            this.post = post;
            this.Event = Event;
        }
    }
}
