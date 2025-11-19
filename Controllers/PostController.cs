using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThirdPartyApiDemo.Models;
using ThirdPartyApiDemo.Services;

namespace ThirdPartyApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly SecretClient _secretClient;
        //private static int _callCount = 0;
        public PostController(IPostService postService, SecretClient secretClient)
        {
            _postService = postService;
            _secretClient = secretClient;
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

        [HttpGet]
        [Route("secret-key")]
        public async Task<ActionResult<string>> GetSecretKey()
        {
            try
            {
                var secret = await _secretClient.GetSecretAsync("DbConnectionString");
                return Ok(new { ConnectionString = secret.Value.Value });
            }
            catch (Exception ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }
    }
}
