namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class SizeListItemViewModel
    {
        public SizeListItemViewModel(int id, int prSize)
        {
            Id = id;
            PrSize = prSize;
        }

        public int Id { get; set; }
        public int PrSize { get; set; }
    }
}
