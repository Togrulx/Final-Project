namespace IydeParfume.Areas.Admin.ViewModels.AboutUsImage
{
    public class AboutUsImageViewModel
    {
        public class ListItem
        {

            public int Id { get; set; }
            public string FileUrl { get; set; }
   
            public DateTime CreatedAt { get; set; }
        }

        public int AboutUsId { get; set; }
        public List<ListItem> Files { get; set; }
    }
}
