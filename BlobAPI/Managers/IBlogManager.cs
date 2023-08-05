using BlobAPI.Commands;
using BlobAPI.Entities;

namespace BlobAPI.Managers
{
    public interface IBlogManager
    {
        /// <summary>
        /// Finds an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to find.</param>
        /// <param name="asNoTracking">Specifies if not to track queried entity.</param>
        /// <returns>The entity with the specified Id, or null if not found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task<Blog?> GetByIdAsync(Guid id, bool asNoTracking = true);
        /// <summary>
        /// Finds an entity by its Header.
        /// </summary>
        /// <param name="header">The Header of the entity to find.</param>
        /// <param name="asNoTracking">Specifies if not to track queried entity.</param>
        /// <returns>The entity with the specified Header, or null if not found.</returns>
        Task<Blog?> GetByHeaderAsync(string? header, bool asNoTracking = true);
        /// <summary>
        /// Creates a new entity and adds it to the database.
        /// </summary>
        /// <param name="command">The argument with specified properties for creating an entity.</param>
        /// <returns>The newly created entity Id.</returns>
        /// <exception cref="BadRequestException"></exception>
        Task<Guid> CreateAsync(CreateBlogCommand command);
        /// <summary>
        /// Updates an existing entity with new data.
        /// </summary>
        /// <param name="command">The argument with specified properties for updatig an entity.</param>
        /// <exception cref="BadRequestException"></exception>
        Task UpdateAsync(UpdateBlogCommand command);
        /// <summary>
        /// Deletes an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to delete.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="BadRequestException"></exception>
        Task DeleteAsync(Guid id);
    }
}
