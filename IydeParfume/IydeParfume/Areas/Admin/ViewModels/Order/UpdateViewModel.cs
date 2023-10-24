using Microsoft.Build.Framework;
using IydeParfume.Database.Models.Enums;

namespace IydeParfume.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
