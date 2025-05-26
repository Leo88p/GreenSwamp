using System.Text.RegularExpressions;

namespace Lab3.Models
{
    public class Post
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; } = null!;
        public string PostType { get; set; } = null!;
        public string? MediaUrl { get; set; }
        public string? MediaType { get; set; }
        public string? AltText { get; set; }
        public string? ThumbnailUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? ParentPostId { get; set; }
        public string ContentWithLinks
        {
            get
            {
                return Regex.Replace(Content, @"#(\w+)",
                    "<a class='text-swamp-700 hover:text-swamp-500' href='/Ponds/Posts/$1'>#$1</a>");
            }
        }

    }
}
