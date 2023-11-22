using BlogOnline.BackEnd.Enum;

namespace BlogOnline.BackEnd.Entity
{
    public class Blog : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? CreatedBy { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IEnumerable<Guid> FileIds { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
