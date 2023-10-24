namespace IydeParfume.Areas.Admin.ViewModels.Brand
{
    public class ListItemViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }


        public ListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
