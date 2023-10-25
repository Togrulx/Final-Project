using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.ViewModels.SupportPayment
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]

        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
