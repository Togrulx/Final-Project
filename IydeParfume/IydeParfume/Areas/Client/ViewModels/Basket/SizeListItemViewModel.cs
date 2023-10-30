namespace IydeParfume.Areas.Client.ViewModels.Basket
{
    public class SizeListItemViewModel
    {
        public int Id { get; set; }
        public int PrSize { get; set; }


        public SizeListItemViewModel(int id, int prSize)
        {
            Id = id;
            PrSize = prSize;
        }
    }
}
