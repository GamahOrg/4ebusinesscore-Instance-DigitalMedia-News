namespace News.Domain.Models
{
    public class Sector : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Description { get; set; }
        public bool IsPublished { get; set; }

    }
}
