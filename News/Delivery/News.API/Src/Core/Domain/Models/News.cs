namespace News.Domain.Models
{
    public class News : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string? TitleEn { get; set; }
        public string Description { get; set; }
        public string?  DescriptionEn { get; set; }
        public string? Url { get; set; }
        public string Brief { get; set; }
        public string ? BriefAr { get; set; }

        public string MainImageUrl { get; set; }
        public Guid SectorId { get; set; }
        public Sector Sector { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsArchive { get; set; }
        public bool IsMainNews { get; set; }
        public bool IsPublished { get; set; }
        public NewsStatus NewsStatus { get; set; }
       // public int ? Lk_NewsStatusId { get; set; }  

      //  public Lk_NewsStatus Lk_NewsStatus { get; set; }

        public virtual ICollection<NewsView> Views { get; set; }
        public virtual ICollection<NewsAttachments>? NewsAttachments { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public virtual ICollection<NewsRate> NewsRates { get; set; }
    }
}
