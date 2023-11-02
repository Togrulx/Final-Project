namespace IydeParfume.Areas.Client.ViewModels.SupportPayment
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string content, string note, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            Note = note;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
