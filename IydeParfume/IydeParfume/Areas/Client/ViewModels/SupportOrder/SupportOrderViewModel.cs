namespace IydeParfume.Areas.Client.ViewModels.SupportOrder
{
    public class SupportOrderViewModel
    {
        public SupportOrderViewModel(int id, string title, string content, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
