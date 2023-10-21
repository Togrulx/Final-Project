using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Contact : BaseEntity<int>, IAuditable
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
