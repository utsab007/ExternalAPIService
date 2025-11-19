using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThirdPartyApiDemo.Models;
using ThirdPartyApiDemo.Services;

namespace ThirdPartyApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        //private static int _callCount = 0;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {
            //_callCount++;
            //if (_callCount < 4) // Fail first 3 attempts
            //{
            //    return StatusCode(500, $"Attempt {_callCount}: Simulated failure");
            //}
            //await Task.Delay(5000); // Simulate delay
            return Ok(await _postService.GetAllAsync());
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post post)
        {
            var created = await _postService.CreateAsync(post);
            return created == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Post post)
        {
            var updated = await _postService.UpdateAsync(id, post);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _postService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
