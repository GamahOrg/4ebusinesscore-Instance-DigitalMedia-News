namespace News.Domain.Models
{
    public class NewsAttachments : BaseEntity<Guid>
    {
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public string ImagePath { get; set; }

    }
}
