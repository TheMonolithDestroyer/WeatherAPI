using BlobAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlobAPI.Persistence
{
    public class BlogContext : DbContext, IBlogContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
