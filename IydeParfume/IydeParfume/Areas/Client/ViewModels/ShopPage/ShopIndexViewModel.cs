namespace IydeParfume.Areas.Client.ViewModels.ShopPage
{
    public class ShopIndexViewModel
    {
        public ShopIndexViewModel(List<CategoryListItemViewModel> categories, List<SizeListItemViewModel> sizes,
            List<SeasonListItemViewModel> seasons, List<BrandListItemViewModel> brands,
            List<GroupListItemViewModel> groups, List<UsageTimeListItemViewModel> usageTimes, List<PriceListItemViewModel> prices)
        {
            Categories = categories;
            Sizes = sizes;
            Seasons = seasons;
            Brands = brands;
            Groups = groups;
            UsageTimes = usageTimes;
            Prices = prices;
        }

        public List<CategoryListItemViewModel> Categories { get; set; }

        public List<SizeListItemViewModel> Sizes { get; set; }
        public List<SeasonListItemViewModel> Seasons { get; set; }

        public List<BrandListItemViewModel> Brands { get; set; }
        public List<GroupListItemViewModel> Groups { get; set; }
        public List<UsageTimeListItemViewModel> UsageTimes { get; set; }
        public List<PriceListItemViewModel> Prices { get; set; }



    }

    public class CategoryListItemViewModel
    {
        public CategoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class SizeListItemViewModel
    {
        public SizeListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class SeasonListItemViewModel
    {
        public SeasonListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class BrandListItemViewModel
    {
        public BrandListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class GroupListItemViewModel
    {
        public GroupListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class UsageTimeListItemViewModel
    {
        public UsageTimeListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class PriceListItemViewModel
    {
        public PriceListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

}

