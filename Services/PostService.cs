using ThirdPartyApiDemo.Http;
using ThirdPartyApiDemo.Models;
using static System.Net.WebRequestMethods;

namespace ThirdPartyApiDemo.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpClientHelper _http;
        private readonly string _baseUrl;

        public PostService(IHttpClientHelper httpClientHelper, IConfiguration configuration)
        {
            _http = httpClientHelper;
            _baseUrl = configuration["PostAPI:BaseUrl"].ToString();
        }
        public async Task<List<Post>> GetAllAsync()
        {
            var result = await _http.GetAsync<List<Post>>(_baseUrl);
            return result ?? new List<Post>();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _http.GetAsync<Post>($"{_baseUrl}/{id}");
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
