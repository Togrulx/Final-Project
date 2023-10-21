using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class AboutUs: BaseEntity<int>,IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<AboutUsImage>? AboutUsImages { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
