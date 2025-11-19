using System.Text.Json;

namespace ThirdPartyApiDemo.Http
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> PostAsync<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> PutAsync<T>(string url, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
