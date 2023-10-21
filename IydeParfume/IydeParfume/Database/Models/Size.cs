using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Size :BaseEntity<int>,IAuditable
    {
        public int PrSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? PrPercent { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
    }
}
