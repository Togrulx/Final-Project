namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class BrandListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public BrandListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
