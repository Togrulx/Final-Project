namespace IydeParfume.Areas.Client.ViewModels.Home
{
    public class SliderViewModel
    {

        public int Id { get; set; }
      
        public string BackGroundImageUrl { get; set; }

        public SliderViewModel(){ }

        public SliderViewModel(int id, string backGroundImageUrl)
        {
            Id = id;
            BackGroundImageUrl = backGroundImageUrl;
        }
    }
}
