namespace IydeParfume.Areas.Admin.ViewModels.Group
{
    public class ListViewModel
    {
       

        public int Id { get; set; }

        public string Title { get; set; }


        public ListViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
