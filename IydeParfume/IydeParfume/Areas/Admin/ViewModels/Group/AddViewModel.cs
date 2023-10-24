using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Group
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
