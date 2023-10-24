namespace IydeParfume.Areas.Admin.ViewModels.Category
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string parentName,string imageUrl)
        {
            Id = id;
            Title = title;
            ParentName = parentName;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ParentName { get; set; }
        public string ImageUrl { get; set; }


    }
}
