using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string? URL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
