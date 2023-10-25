using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Size
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public int PrSize { get; set; }
        public int? PrPercent { get; set; }
    }
}
