namespace IydeParfume.Areas.Admin.ViewModels.Season
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
      
    }
}
