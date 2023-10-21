using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductBrand :BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
