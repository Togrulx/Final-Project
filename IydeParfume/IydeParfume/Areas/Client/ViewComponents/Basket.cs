using IydeParfume.Areas.Client.ViewModels.Shop;
using IydeParfume.Areas.Client.ViewModels.Basket;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Basket")]

    public class Basket: ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public Basket(DataContext dataContext, IUserService userService = null!, IFileService fileService = null!)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<BasketCookieViewModel>? viewModels = null)
        {

            if (_userService.IsAuthenticated)
            {

                var model = await _dataContext.BasketProducts.Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                    .Select(p =>
                    new BasketCookieViewModel(p.Product!.Id, p.Product.Name, p.Product.ProductImages!.Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.Product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameFileSystem, Contracts.File.UploadDirectory.Products)
                    : String.Empty,
                        p.Quantity,
                        p.SizeId,

                        _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == p.Product.Id)
                       .Select(ps => new SizeListItemViewModel(ps.Size.Id, ps.Size.PrSize)).ToList(),
                        p.SizeId != null
                        ? _dataContext.Sizes.FirstOrDefault(s => s.Id == p.SizeId)!.PrSize
                        : _dataContext.Sizes.FirstOrDefault()!.PrSize,
                        (decimal)p.CurrentPrice,(decimal)p.CurrentPrice*(decimal)p.Quantity)).ToListAsync();

                return View(model);
            }


            if (viewModels is not null)
            {
                return View(viewModels);
            }

            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<BasketCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
