using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace IydeParfume.Areas.Client.ViewModels.Contact
{
    public class SupportOrderViewModel
    {

        [Required]
        public string FirstName { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
