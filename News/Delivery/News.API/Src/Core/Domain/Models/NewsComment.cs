
namespace News.Domain.Models
{
    public class NewsComment : BaseEntity<Guid>
    {
        public string Description { get; set; }

        public Guid NewsId { get; set; }
        public News News { get; set; }

        public Guid? ParentId { get; set; } 
        public NewsComment Parent { get; set; }
        public virtual ICollection<NewsComment> Replies { get; set; }
    }
}
