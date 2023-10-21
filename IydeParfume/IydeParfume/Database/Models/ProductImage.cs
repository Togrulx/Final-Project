using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductImage : BaseEntity<int>, IAuditable
    {
        public string ImageNames { get; set; }
        public string ImageNameFileSystem { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
