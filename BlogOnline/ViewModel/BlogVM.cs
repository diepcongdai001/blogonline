namespace BlogOnline.ViewModel
{
    public class BlogVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public int Priority { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
