namespace IydeParfume.Areas.Admin.ViewModels.Contact
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public ListItemViewModel(int id, string firstName, string phoneNumber, string email, string content, DateTime createdAt)
        {
            Id = id;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
            Email = email;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}
