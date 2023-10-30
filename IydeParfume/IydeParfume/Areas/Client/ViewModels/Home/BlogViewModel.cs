namespace IydeParfume.Areas.Client.ViewModels.Home
{
    public class BlogViewModel
    {
        public BlogViewModel(int id, string title, string content, string blogDisplays, /*bool isImage, bool isVideo,*/ DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            BlogDisplays = blogDisplays;
            //IsImage = isImage;
            //IsVideo = isVideo;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BlogDisplays { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public DateTime CreatedAt { get; set; }

   


       
    }
}
