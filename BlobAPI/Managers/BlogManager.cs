using BlobAPI.Commands;
using BlobAPI.Engine.Exceptions;
using BlobAPI.Entities;
using BlobAPI.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BlobAPI.Managers
{
    public class BlogManager : IBlogManager
    {
        private readonly ILogger<BlogManager> _logger;
        private readonly IBlogContext _context;
        public BlogManager(
            ILogger<BlogManager> logger,
            IBlogContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Finds an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to find.</param>
        /// <param name="asNoTracking">Specifies if not to track queried entity.</param>
        /// <returns>The entity with the specified Id, or null if not found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<Blog?> GetByIdAsync(Guid id, bool asNoTracking = true)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(id));

            var blog = _context.Blogs.AsQueryable();
            if (asNoTracking)
                blog = blog.AsNoTracking();

            return await blog
                .Where(i => !i.DeletedAt.HasValue && i.Id == id)
                .OrderByDescending(i => i.CreatedAt)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds an entity by its Header.
        /// </summary>
        /// <param name="header">The Header of the entity to find.</param>
        /// <param name="asNoTracking">Specifies if not to track queried entity.</param>
        /// <returns>The entity with the specified Header, or null if not found.</returns>
        public async Task<Blog?> GetByHeaderAsync(string? header, bool asNoTracking = true)
        {
            var blog = _context.Blogs.AsQueryable();
            if (asNoTracking)
                blog = blog.AsNoTracking();
            
            return await blog
                .Where(i => !i.DeletedAt.HasValue && i.Header == header)
                .OrderByDescending(i => i.CreatedAt)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new entity and adds it to the database.
        /// </summary>
        /// <param name="command">The argument with specified properties for creating an entity.</param>
        /// <returns>The newly created entity Id.</returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<Guid> CreateAsync(CreateBlogCommand command)
        {
            var validationResult = new CreateBlogCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var blog = await GetByHeaderAsync(command.BlogHeader);
            if (blog != null)
                throw new BadRequestException("There is already exists a blog with similar header.");

            blog = new Blog
            {
                Header = command.BlogHeader,
                Content = command.BlogContent
            };
            
            await _context.Blogs.AddAsync(blog);
            await _context.SaveAsync();

            _logger.LogInformation($"Successfully created a new blog entity with id: {blog.Id}");

            return blog.Id;
        }

        /// <summary>
        /// Updates an existing entity with new data.
        /// </summary>
        /// <param name="command">The argument with specified properties for updatig an entity.</param>
        /// <exception cref="BadRequestException"></exception>
        public async Task UpdateAsync(UpdateBlogCommand command)
        {
            var validationResult = new UpdateBlogCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var blog = await GetByIdAsync(command.Id, false);
            if (blog == null)
                throw new BadRequestException($"Not found a blog by id '{command.Id}'.");

            blog.UpdatedAt = DateTime.Now;
            blog.Header = command.BlogHeader;
            blog.Content = command.BlogContent;

            _logger.LogInformation($"Successfully update the blog entity with id: {blog.Id}");

            await _context.SaveAsync();
        }

        /// <summary>
        /// Deletes an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to delete.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="BadRequestException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(id));

            var blog = await GetByIdAsync(id, false);
            if (blog == null)
                throw new BadRequestException($"Not found a blog by id.");

            blog.UpdatedAt = DateTime.UtcNow;
            blog.DeletedAt = DateTime.UtcNow;
            await _context.SaveAsync();

            _logger.LogInformation($"Successfully deleted the blog entity with id: {id}");
        }
    }
}
