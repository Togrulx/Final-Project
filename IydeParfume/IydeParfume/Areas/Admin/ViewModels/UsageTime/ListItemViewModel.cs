﻿namespace IydeParfume.Areas.Admin.ViewModels.UsageTime
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
