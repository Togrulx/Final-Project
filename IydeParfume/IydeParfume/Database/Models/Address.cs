using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Address: BaseEntity<int>, IAuditable
    {
        public string AddressName { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public int UserId { get; set; } 
    }
}
