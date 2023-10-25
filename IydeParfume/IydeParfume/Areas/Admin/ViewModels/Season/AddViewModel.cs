using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Season
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
