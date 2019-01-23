using DemoApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Web.Data
{
  public class BloggingContext : DbContext
  {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BloggingContext(DbContextOptions<BloggingContext> options)
          : base(options)
    { }
  }
}