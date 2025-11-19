using ThirdPartyApiDemo.Http;
using ThirdPartyApiDemo.Models;
using static System.Net.WebRequestMethods;

namespace ThirdPartyApiDemo.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpClientHelper _http;
        private readonly string _baseUrl;
        private readonly ILogger<PostService> _logger;

        public PostService(IHttpClientHelper httpClientHelper, IConfiguration configuration,ILogger<PostService> logger)
        {
            _http = httpClientHelper;
            _baseUrl = Convert.ToString(configuration["PostAPI:BaseUrl"]) ?? "";
            _logger = logger;

        }
        public async Task<List<Post>> GetAllAsync()
        {
            var result = await _http.GetAsync<List<Post>>(_baseUrl);
            _logger.LogInformation("Fetched {Count} posts", result?.Count ?? 0);

            return result ?? new List<Post>();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var data = await _http.GetAsync<Post>($"{_baseUrl}/{id}");
            if (data != null)
            {
                _logger.LogInformation("Fetched post with ID {Id}", id);
            }
            else
            {
                _logger.LogWarning("Post with ID {Id} not found", id);
            }
            return data;
        }

        public async Task<Post?> CreateAsync(Post post)
        {
            return await _http.PostAsync<Post>(_baseUrl, post);
        }

        public async Task<Post?> UpdateAsync(int id, Post post)
        {
            return await _http.PutAsync<Post>($"{_baseUrl}/{id}", post);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"{_baseUrl}/{id}");
        }
    }
}
