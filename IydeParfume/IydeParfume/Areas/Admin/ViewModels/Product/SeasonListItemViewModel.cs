namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class SeasonListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public SeasonListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
