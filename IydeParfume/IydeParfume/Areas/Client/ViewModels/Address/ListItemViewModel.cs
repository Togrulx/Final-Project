namespace IydeParfume.Areas.Client.ViewModels.Address
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string addressName, string fullAddress, string city, string phone)
        {
            Id = id;
            AddressName = addressName;
            FullAddress = fullAddress;
            City = city;
            Phone = phone;
        }

        public int Id { get; set; }
        public string AddressName { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
