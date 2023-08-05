using BlobAPI.Commands;
using BlobAPI.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlobAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogManager _manager;
        public BlogController(IBlogManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager)); ;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            var result = await _manager.GetByIdAsync(id);
            var status = result == null ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            return StatusCode((int)status, Result.Succeed(result, status));
        }

        [HttpGet]
        public async Task<IActionResult> GetByTopicName([FromQuery]string? header)
        {
            var result = await _manager.GetByHeaderAsync(header);
            var status = result == null ? HttpStatusCode.NoContent : HttpStatusCode.OK;
            return StatusCode((int)status, Result.Succeed(result, status));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBlogCommand command)
        {
            var result = await _manager.CreateAsync(command);
            return StatusCode((int)HttpStatusCode.Created, Result.Succeed(result, HttpStatusCode.Created));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateBlogCommand command)
        {
            await _manager.UpdateAsync(command);
            return Ok(Result.Succeed());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            await _manager.DeleteAsync(id);
            return Ok(Result.Succeed());
        }
    }
}
