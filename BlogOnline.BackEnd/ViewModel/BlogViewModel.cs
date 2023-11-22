using BlogOnline.BackEnd.Entity;

namespace BlogOnline.BackEnd.ViewModel
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public int Priority { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        // public List<FileUpload> FileUploads { get; set; } = new List<FileUpload>();
    }
}
