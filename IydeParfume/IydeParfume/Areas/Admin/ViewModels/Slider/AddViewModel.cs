using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        [Required]
        public IFormFile? BackgroundImage { get; set; }
        public string? BackgroundImageUrl { get; set; }
   
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
