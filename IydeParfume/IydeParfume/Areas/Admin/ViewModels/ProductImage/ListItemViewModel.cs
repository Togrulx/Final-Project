namespace IydeParfume.Areas.Admin.ViewModels.ProductImage
{
    public class ListItemViewModel
    {
        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }
        public int ProductId { get; set; }
        public List<ListItem>? Images { get; set; }
    }
}
