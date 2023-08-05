using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BlobAPI.Entities;

namespace BlobAPI.Persistence
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public virtual void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(p => p.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(p => p.Header).IsRequired().HasMaxLength(256);
            builder.HasIndex(p => p.Header);
        }
    }
}
