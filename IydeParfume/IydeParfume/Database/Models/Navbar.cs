using IydeParfume.Database.Models.Common;

namespace IydeParfume.Database.Models
{
    public class Navbar :BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string URL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<SubNavbar>? SubNavbars { get; set; }

    }
}
