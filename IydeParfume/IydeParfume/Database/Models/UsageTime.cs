using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class UsageTime : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductUsageTime> ProductUsageTimes { get; set; }
    }
}
