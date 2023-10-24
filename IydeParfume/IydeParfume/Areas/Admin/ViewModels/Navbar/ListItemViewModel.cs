namespace IydeParfume.Areas.Admin.ViewModels.Navbar
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public ListItemViewModel(int id, string? name, string? URL, int order, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Url = URL;
            Order = order;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
