namespace IydeParfume.Areas.Client.ViewModels.AboutUs
{
    public class CertificateViewModel
    {
        public CertificateViewModel(int id, string title, string backGroundImageUrl)
        {
            Id = id;
            Title = title;
            BackGroundImageUrl = backGroundImageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string BackGroundImageUrl { get; set; }
    }
}
