using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string BackgroundImage { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
