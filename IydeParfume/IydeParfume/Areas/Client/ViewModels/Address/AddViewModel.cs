using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Client.ViewModels.Address
{
    public class AddViewModel
    {
        
        [Required]
        public string AddressName { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
