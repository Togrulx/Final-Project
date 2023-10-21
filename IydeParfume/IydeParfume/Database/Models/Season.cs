using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Season : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductSeason> ProductSeasons { get; set; }
    }
}
