using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class SupportPayment : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
