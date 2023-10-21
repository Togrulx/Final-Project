using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class AboutUsImage : BaseEntity<int>, IAuditable
    {
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }

        public int AboutUsId { get; set; }
        public AboutUs AboutUs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
