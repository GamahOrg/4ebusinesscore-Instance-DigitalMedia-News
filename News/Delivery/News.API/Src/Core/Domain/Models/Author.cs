namespace News.Domain.Models
{
    public class Author : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public bool IsPublisher { get; set; }

    }
}
