namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class UsageTimeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public UsageTimeListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
