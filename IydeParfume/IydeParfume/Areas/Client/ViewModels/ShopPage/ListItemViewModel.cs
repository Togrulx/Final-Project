namespace IydeParfume.Areas.Client.ViewModels.ShopPage
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MainImgUrl { get; set; }
        public List<BrandListItemViewModel> Brand { get; set; }



     




        public ListItemViewModel() { }

        public ListItemViewModel(int id, string name, string description, decimal price, DateTime createdAt,
            string mainImgUrl, List<BrandListItemViewModel> brand)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            MainImgUrl = mainImgUrl;
            Brand = brand;
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
    }
}
