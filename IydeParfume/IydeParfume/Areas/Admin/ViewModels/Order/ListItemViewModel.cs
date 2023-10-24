using IydeParfume.Database.Models.Enums;

namespace IydeParfume.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public string UserEmail { get; set; }
		public ListItemViewModel(string id, DateTime createdAt, OrderStatus status, decimal total, string userEmail)
		{
			Id = id;
			CreatedAt = createdAt;
			Status = status;
			Total = total;
			UserEmail = userEmail;
		}

	}
}
