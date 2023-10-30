using IydeParfume.Areas.Client.ViewModels.Basket;
using IydeParfume.Areas.Client.ViewModels.Shop;
using IydeParfume.Database.Models;

namespace IydeParfume.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product, ShopViewModel model);

    }
}
