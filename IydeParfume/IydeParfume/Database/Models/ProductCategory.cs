
using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductCategory :BaseEntity<int> 
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int CategoryId { get; set; }
        public Category? Category { get; set; }



    }
}
