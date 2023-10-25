using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Size
{
    public class AddViewModel
    {
        [Required]
        public int PrSize { get; set; }
        [Required]
        public int PrPercent { get; set; }
    }
}
