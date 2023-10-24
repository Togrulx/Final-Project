namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string name, string description, decimal price, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
