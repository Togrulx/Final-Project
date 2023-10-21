using IydeParfume.Database.Models.Common;
using IydeParfume.Database.Models.Enums;

namespace IydeParfume.Database.Models
{
    public class Order :BaseEntity<int>, IAuditable
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal SumTotalPrice { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

    }
}
