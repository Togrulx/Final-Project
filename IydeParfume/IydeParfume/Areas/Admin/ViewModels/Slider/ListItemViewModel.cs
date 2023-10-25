namespace IydeParfume.Areas.Admin.ViewModels.Slider
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string backgroundImageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            BackgroundImageUrl = backgroundImageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int Id { get; set; }
     
        public string BackgroundImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
