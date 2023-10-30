using IydeParfume.Areas.Client.ViewModels.Basket;

namespace IydeParfume.Areas.Client.ViewModels.Basket
{
    public class BasketCookieViewModel
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public int DisCountPrice { get; set; }
        public decimal? Total { get; set; }
        public List<SizeListItemViewModel> Sizes { get; set; }
        public int? SizeId { get; set; }
        public int? PrSize { get; set; }
        public BasketCookieViewModel(int id, string? title, string? imageUrl, int quantity, int? sizeId, List<SizeListItemViewModel> sizes, int? prSize, decimal? price, decimal? total)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
            SizeId = sizeId;
            Sizes = sizes;
            PrSize = prSize;
        }

        public BasketCookieViewModel()
        {

        }
        public BasketCookieViewModel(int id, string? title, string? imageUrl, int quantity, int? sizeId,
            List<SizeListItemViewModel> sizes,
            int? prSize, int disCountPrice, decimal total)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            DisCountPrice = disCountPrice;
            Total = total;
            SizeId = sizeId;
            Sizes = sizes;
            PrSize = prSize;
        }
    }
}
