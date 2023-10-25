namespace IydeParfume.Areas.Admin.ViewModels.Size
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, int prSize, int? prPercent)
        {
            Id = id;
            PrSize = prSize;
            PrPercent = prPercent;
        }

        public int Id { get; set; }
        public int PrSize { get; set; }
        public int? PrPercent { get; set; }
    }
}
