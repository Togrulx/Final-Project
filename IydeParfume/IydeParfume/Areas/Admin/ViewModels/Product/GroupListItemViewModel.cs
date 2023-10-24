namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class GroupListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public GroupListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
