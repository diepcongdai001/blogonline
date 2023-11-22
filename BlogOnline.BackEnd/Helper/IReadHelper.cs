namespace BlogOnline.BackEnd.Helper
{
    public interface IReadHelper
    {
        List<T> ExecuteResultFromQuery<T>(ApplicationDbContext context, string query);
    }
}
