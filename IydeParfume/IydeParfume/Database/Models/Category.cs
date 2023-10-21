using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Category : BaseEntity<int>,IAuditable
    {
        public string? Title { get; set; }
        public int? ParentId { get; set; }
        public string Image { get; set; }
        public string ImageInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get ; set; }


        public Category? Parent { get; set; }
        public List<Category>? Categories { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }


    }
}
