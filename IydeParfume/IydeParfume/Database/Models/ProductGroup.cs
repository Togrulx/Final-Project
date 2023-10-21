using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductGroup : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
