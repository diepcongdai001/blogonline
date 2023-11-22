using BlogOnline.ViewModel;

namespace BlogOnline.Services
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetBlogs();
    }
}
