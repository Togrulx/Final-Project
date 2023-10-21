using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductUsageTime : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int UsageTimeId { get; set; }
        public UsageTime? UsageTime { get; set; }
    }
}
