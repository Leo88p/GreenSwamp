using Lab3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Data
{
    public class SwampContext : IdentityDbContext<Auth, IdentityRole<long>, long>
    {
        public SwampContext(DbContextOptions<SwampContext> options)
            : base(options)
        {
        }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Auth>().ToTable("Auth");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Interaction>().ToTable("Interaction");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
