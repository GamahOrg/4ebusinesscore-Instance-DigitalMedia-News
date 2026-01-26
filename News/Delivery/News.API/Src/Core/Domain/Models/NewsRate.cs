namespace News.Domain.Models
{
    public class NewsRate : BaseEntity<Guid>
    {
       
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public int RateValue { get; set; }
        
    }
}
