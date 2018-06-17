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

            modelBuilder.Entity<Post>()
                .HasIndex(p => new { p.Permalink })
                .IsUnique(true);
        }

        public DbSet<Post> Posts { get; set; }
    }
}