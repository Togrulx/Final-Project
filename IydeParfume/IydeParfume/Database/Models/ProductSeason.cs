using Azure;
using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class ProductSeason : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int SeasonId { get; set; }
        public Season? Season { get; set; }
    }
}
