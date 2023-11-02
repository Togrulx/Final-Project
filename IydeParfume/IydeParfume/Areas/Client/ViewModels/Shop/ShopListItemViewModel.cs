namespace IydeParfume.Areas.Client.ViewModels.Shop
{
    public class ShopListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MainImgUrl { get; set; }


        

        public ShopListItemViewModel()
        {

        }

        public ShopListItemViewModel(int id, string name, string description, decimal price, DateTime createdAt, string mainImgUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            MainImgUrl = mainImgUrl;
        }
    }
}
