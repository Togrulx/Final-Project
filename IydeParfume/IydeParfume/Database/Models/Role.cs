using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Role :BaseEntity<int>
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
