using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Brand
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
