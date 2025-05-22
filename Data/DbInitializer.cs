using Lab3.Models;
using System;
namespace Lab3.Data
{
    public class DbInitializer
    {
        public static void Initialize(SwampContext context)
        {
            if (context.Posts.Any() || context.Events.Any() || context.Auths.Any() || context.Interactions.Any() || context.Tags.Any() || context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User
                {
                    Username = "@user1",
                    DisplayName = "John Doe",
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://i.pravatar.cc/100?u=swa@pravatar.com"
                },
                new User
                {
                    Username = "@user2",
                    DisplayName = "Jane Smith",
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://i.pravatar.cc/100?u=swamp@pravatar.com"
                },
                new User
                {
                    Username = "@user3",
                    DisplayName = "Tereshchenko Oleg",
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://i.pravatar.cc/100?u=Olejka@pravatar.com"
                },
                new User
                {
                    Username = "@user4",
                    DisplayName = "VetTerOk",
                    CreatedAt = DateTime.Now,
                    AvatarUrl = "https://i.pravatar.cc/100?u=BBB@pravatar.com"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            var tags = new Tag[]
            {
                new Tag { TagName = "swamp", UsageCount = 0 },
                new Tag { TagName = "frog", UsageCount = 0 },
                new Tag { TagName = "tree", UsageCount = 0 }
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            var posts = new Post[]
            {
                new Post
                {
                    UserId = users[0].UserId,
                    Content = "My first post!",
                    PostType = "text",
                    CreatedAt = DateTime.Now.AddHours(3),
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=swa@pravatar.com",
                },
                new Post
                {
                    UserId = users[1].UserId,
                    Content = "I like swamp!",
                    PostType = "text",
                    CreatedAt = DateTime.Now.AddMinutes(33),
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=swamp@pravatar.com"
                },
                new Post
                {
                    UserId = users[2].UserId,
                    Content = "I like frogs!",
                    PostType = "text",
                    CreatedAt = DateTime.Now.AddHours(1),
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=Olejka@pravatar.com"
                },
                new Post
                {
                    UserId = users[3].UserId,
                    Content = "I don't like frogs and swamp!",
                    PostType = "text",
                    CreatedAt = DateTime.Now.AddMinutes(12),
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=BBB@pravatar.com"
                },
                new Post
                {
                    UserId = users[1].UserId,
                    Content = "Check out this amazing sunset view! 🌅",
                    PostType = "photo",
                    MediaUrl = "~/images/4x3_Photo_of_a_messy_desk_with_table.png",
                    MediaType = "image/jpeg",
                    AltText = "Sunset over mountains with reflective lake",
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=swamp@pravatar.com",
                    CreatedAt = DateTime.Now.AddHours(22),
                },
                new Post
                {
                    UserId = users[2].UserId,
                    Content = "Just posted my new travel vlog! 🎥",
                    PostType = "video",
                    MediaUrl = "~/images/Standard_Mode_a_looed_gif_of_a_pixel_frog_flyi.mp4",
                    MediaType = "video/mp4",
                    AltText = "Drone footage of tropical beach",
                    ThumbnailUrl = "https://i.pravatar.cc/100?u=Olejka@pravatar.com",
                    CreatedAt = DateTime.Now.AddMinutes(7)
                }
            };
            context.Posts.AddRange(posts);
            context.SaveChanges();

            var events = new Event[]
            {
                new Event
                {
                    PostId = posts[0].PostId,
                    EventTime = DateTime.Now.AddDays(7),
                    Location = "Central Park, New York",
                    HostOrg = "Tech Community",
                    RsvpCount = 150,
                    MaxCapacity = 200
                },
                new Event
                {
                    PostId = posts[2].PostId,
                    EventTime = DateTime.Now.AddMonths(1),
                    Location = "Online",
                    HostOrg = "Digital Nomads",
                    RsvpCount = 85,
                    MaxCapacity = 300
                },
                new Event
                {
                    PostId = posts[3].PostId,
                    EventTime = DateTime.Now.AddYears(1),
                    Location = "Paris, France",
                    MaxCapacity = 50
                }
            };
            context.Events.AddRange(events);
            context.SaveChanges();

            var interactions = new Models.Interaction[]
            {
                new Models.Interaction
                {
                    PostId = posts[0].PostId,
                    UserId = users[1].UserId,
                    Content = "Wow",
                    CreatedAt = DateTime.Now.AddHours(1)
                },
                new Models.Interaction
                {
                    PostId = posts[0].PostId,
                    UserId = users[1].UserId,
                    CreatedAt = DateTime.Now.AddHours(2)
                },
                new Models.Interaction
                {
                    PostId = posts[1].PostId,
                    UserId = users[1].UserId,
                    CreatedAt = DateTime.Now.AddHours(3)
                }
                ,
                new Models.Interaction
                {
                    PostId = posts[1].PostId,
                    UserId = users[1].UserId,
                    Content = "Cool",
                    CreatedAt = DateTime.Now.AddHours(4)
                }
                ,
                new Models.Interaction
                {
                    PostId = posts[1].PostId,
                    UserId = users[2].UserId,
                    CreatedAt = DateTime.Now.AddHours(5)
                }
                ,
                new Models.Interaction
                {
                    PostId = posts[2].PostId,
                    UserId = users[1].UserId,
                    Content = "Great",
                    CreatedAt = DateTime.Now.AddHours(6)
                },
                new Models.Interaction
                {
                    PostId = posts[2].PostId,
                    UserId = users[1].UserId,
                    CreatedAt = DateTime.Now.AddHours(7)
                },
                new Models.Interaction
                {
                    PostId = posts[3].PostId,
                    UserId = users[1].UserId,
                    Content = "Fantastic!",
                    CreatedAt = DateTime.Now.AddHours(8)
                }
            };
            context.Interactions.AddRange(interactions);
            context.SaveChanges();
        }
    }
}
