using BlogOnline.ViewModel;

namespace BlogOnline.Services
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _client;

        public BlogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<BlogVM>> GetBlogs()
        {
            var response = await _client.GetAsync("api/Blogs");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<BlogVM>>();
        }
    }
}
