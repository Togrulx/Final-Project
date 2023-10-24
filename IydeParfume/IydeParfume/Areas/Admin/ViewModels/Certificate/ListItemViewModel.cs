namespace IydeParfume.Areas.Admin.ViewModels.Certificate
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string backgroundImageInFileSystem, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Title = title;
            BackgroundImageInFileSystem = backgroundImageInFileSystem;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
