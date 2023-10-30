using IydeParfume.Areas.Client.ViewModels.Basket;
using IydeParfume.Areas.Client.ViewModels.Shop;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IydeParfume.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;



        public BasketService(
            DataContext dataContext,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }
        public async Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product, ShopViewModel model)
        {
            model = new ShopViewModel
            {
                SizeId = model.SizeId != null ? model.SizeId : _dataContext.Sizes.FirstOrDefault()!.Id,
                Quantity = model.Quantity != 0 ? model.Quantity : 1,
            };

            var allSize = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == model.SizeId);

            var increasePrice = (product.Price * allSize!.PrPercent) / 100;
            var sizePrice = product.Price + increasePrice;


            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();
                return new List<BasketCookieViewModel>();

            }

            return AddCookie();



            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                       .Include(b => b.Basket)
                       .FirstOrDefaultAsync(bp => 
                       bp.Basket!.UserId == _userService.CurrentUser.Id && //bu lazim deyil axi((
                       bp.ProductId == product.Id &&
                       bp.SizeId == model.SizeId);

               

                if (basketProduct is null || basketProduct.SizeId != model.SizeId)
                {
                    var basket = await _dataContext.Baskets.FirstAsync(p => p.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = model.Quantity,
                        BasketId = basket.Id,
                        ProductId = product.Id,
                        SizeId = model.SizeId,
                        CurrentPrice = sizePrice
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }
                else
                {
                    basketProduct.Quantity++;
                }
                await _dataContext.SaveChangesAsync();
            }

            List<BasketCookieViewModel> AddCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];

                var productCookieViewModel = productCookieValue is not null
                   ? JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue)
                   : new List<BasketCookieViewModel> { };

                var cookieViewModel = productCookieViewModel!.FirstOrDefault(pc =>
                pc.Id == product.Id && pc.SizeId == model.SizeId);

                

                if (cookieViewModel is null || cookieViewModel.SizeId != model.SizeId)
                {

                    productCookieViewModel!.Add
                          (new BasketCookieViewModel(product.Id, product.Name, product.ProductImages!.Take(1).FirstOrDefault() != null
                              ? _fileService.GetFileUrl(product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameFileSystem, Contracts.File.UploadDirectory.Products)
                                  : string.Empty,
                                       model.Quantity,
                                       model.SizeId,
                                      _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                                             .Select(ps => new SizeListItemViewModel(ps.Size.Id, ps.Size!.PrSize)).ToList(),
                                         model.SizeId != null
                                         ? _dataContext.Sizes.FirstOrDefault(s => s.Id == model.SizeId)!.PrSize
                                         : _dataContext.Sizes.FirstOrDefault()!.PrSize,
                                         sizePrice != null ? sizePrice : product.Price,
                                          (decimal)model.Quantity * sizePrice 
                                         ));

                }
                else
                {

                        cookieViewModel.Quantity = model.Quantity != null ? cookieViewModel.Quantity += model.Quantity : cookieViewModel.Quantity += 1;
                        cookieViewModel.Total = cookieViewModel.Quantity * cookieViewModel.Price;

                    

                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));

                return productCookieViewModel;
            }
        }
    }
}
