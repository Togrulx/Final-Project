using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.UsageTime
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
