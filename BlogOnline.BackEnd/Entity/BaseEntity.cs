using System.ComponentModel.DataAnnotations;

namespace BlogOnline.BackEnd.Entity
{
    public abstract class BaseEntity<TId>
    {
        public virtual TId Id { get; set; }

        [Timestamp]
        public virtual byte[] Version { get; set; }
    }
}
