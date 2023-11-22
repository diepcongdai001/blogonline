namespace BlogOnline.BackEnd.Entity
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
