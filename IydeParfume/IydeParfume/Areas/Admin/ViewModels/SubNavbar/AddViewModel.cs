using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.SubNavbar
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public int Order { get; set; }
        public string URL { get; set; }
    

        [Required]
        public int NavbarId { get; set; }
        public List<NavbarListItemViewModel>? Navbar { get; set; }
    }
}
