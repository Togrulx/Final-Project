namespace IydeParfume.Areas.Client.ViewModels.Home
{
    public class CategoryViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }
        public string BImageUrl { get; set; }
        public CategoryViewModel(int id, string title, string ImageUrl)
        {
            Id = id;
            Title = title;
            BImageUrl = ImageUrl;
        }
    }
}
