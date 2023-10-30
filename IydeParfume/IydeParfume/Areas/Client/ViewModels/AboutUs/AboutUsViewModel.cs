namespace IydeParfume.Areas.Client.ViewModels.AboutUs
{
    public class AboutUsViewModel
    {
        public AboutUsViewModel(int id, string title, string content, string aboutUsImages, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            AboutUsImages = aboutUsImages;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AboutUsImages { get; set; }
        public DateTime CreatedAt { get; set; }


    }

}
