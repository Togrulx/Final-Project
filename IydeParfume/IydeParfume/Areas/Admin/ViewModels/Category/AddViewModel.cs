using IydeParfume.Areas.Admin.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Category
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryIds { get; set; }
        public List<CatagoryListItemViewModel>? Catagories { get; set; }
    }
}
