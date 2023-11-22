namespace BlogOnline.BackEnd.Entity
{
    public class FileUpload : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public Guid? CreatedBy { get; set; }
    }
}
