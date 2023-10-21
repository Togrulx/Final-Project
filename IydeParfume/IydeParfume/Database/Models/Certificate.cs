using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Certificate : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
