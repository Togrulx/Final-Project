using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Certificate
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
        public IFormFile? BackgroundImage { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
