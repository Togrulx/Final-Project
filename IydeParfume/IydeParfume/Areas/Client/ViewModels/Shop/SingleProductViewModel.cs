using IydeParfume.Areas.Client.ViewModels.Shop;

namespace IydeParfume.Areas.Client.ViewModels.Shop
{
    public class SingleProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Images>? ImgUrl { get; set; }
        public int? SizeId { get; set; }
        public int? PrSize { get; set; }
        public List<SizeViewModeL>? Sizes { get; set; }
        public int Quantity { get; set; }

        public List<CategoryViewModeL>? Categories { get; set; }
        public List<SeasonViewModel>? Seasons { get; set; }
        public List<BrandViewModel>? Brands { get; set; }
        public List<GroupViewModel>? Groups { get; set; }
        public List<UsageTimeViewModel>? UsageTimes { get; set; }
        public List<ShopListItemViewModel> Products { get; set; }

        public SingleProductViewModel(int id, string title, string description, decimal price, List<Images>? imgUrl, List<SizeViewModeL>? sizes,
            int quantity, List<CategoryViewModeL>? categories, List<SeasonViewModel>? seasons = null, List<BrandViewModel>? brands = null, List<GroupViewModel>? groups = null, List<UsageTimeViewModel>? usageTimes = null)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = imgUrl;
            Sizes = sizes;
            Quantity = quantity;
            Categories = categories;
            Seasons = seasons;
            Brands = brands;
            Groups = groups;
            UsageTimes = usageTimes;
        }

        public SingleProductViewModel(int? sizeId, int? prSize)
        {
            SizeId = sizeId;
            PrSize = prSize;
        }
        public SingleProductViewModel()
        {
        }



        public class Images
        {
            public Images()
            {

            }
            public Images(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl;
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

        public class SizeViewModeL
        {
            public SizeViewModeL()
            {

            }
            public SizeViewModeL(int title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public int Title { get; set; }
        }

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }
            public CategoryViewModeL()
            {

            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class SeasonViewModel
        {
            public SeasonViewModel()
            {

            }
            public SeasonViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class BrandViewModel
        {
            public BrandViewModel()
            {

            }
            public BrandViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class GroupViewModel
        {
            public GroupViewModel()
            {

            }
            public GroupViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class UsageTimeViewModel
        {
            public UsageTimeViewModel()
            {

            }
            public UsageTimeViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}
