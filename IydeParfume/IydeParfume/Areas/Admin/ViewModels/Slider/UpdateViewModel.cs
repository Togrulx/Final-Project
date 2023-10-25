using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public IFormFile BackgroundImage { get; set; }
        public string? BackgroundImageUrl { get; set; }
  
        public DateTime UpdatedAt { get; set; }
    }
}
