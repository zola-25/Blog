using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.Data.Models
{
    public class BlogDbContext : IdentityDbContext<BlogAdminUser,BlogAdminRole, Guid>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().Property(p=> p.Title).IsRequired();
            modelBuilder.Entity<Post>().Property(p=> p.UrlSegment).IsRequired();
            modelBuilder.Entity<Post>().Property(p=> p.Html).IsRequired();
            modelBuilder.Entity<Post>().Property(p=> p.CreationDate).IsRequired();

            modelBuilder.Entity<Post>()
                .HasIndex(p => new { p.UrlSegment })
                .IsUnique(true);
        }

        public DbSet<Post> Posts { get; set; }
    }
}