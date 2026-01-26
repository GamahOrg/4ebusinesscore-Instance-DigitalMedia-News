
namespace News.Domain.Models
{
    public class NewsView : BaseEntity<Guid>
    {
        public Guid NewsId { get; set; }
        public News News { get; set; }
    }
}
