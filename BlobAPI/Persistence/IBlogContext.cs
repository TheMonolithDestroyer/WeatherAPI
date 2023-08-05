using BlobAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlobAPI.Persistence
{
    public interface IBlogContext
    {
        DbSet<Blog> Blogs { get; set; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
