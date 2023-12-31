﻿namespace IydeParfume.Areas.Admin.ViewModels.SubNavbar
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string URL { get; set; }
        public string Navbar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListItemViewModel(int id, string name, string url, int order, string navbar, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            URL = url;
            Order = order;
            Navbar = navbar;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
