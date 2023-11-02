using IydeParfume.Areas.Client.ViewModels.Shop;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ShopController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "client-shop-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductSizes).Include(p=>p.ProductCategories).FirstOrDefaultAsync(p => p.Id == id);

            //var basket = await _dataContext.BasketProducts.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null) return NotFound();
      

            var model = new ShopViewModel(product.Id, product.Name!, product.Description!, product.Price, 
               product.ProductImages!
                .Select(p => new ShopViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Products))).ToList(),

                _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.SizeViewModeL(ps.Size.PrSize, ps.Size.Id)).ToList(),
                 1,
                 _dataContext.ProductCategories.Include(ps => ps.Category).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.CategoryViewModeL(ps.Category.Title, ps.Category.Id)).ToList(),

                  _dataContext.ProductSeasons.Include(ps => ps.Season).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.SeasonViewModel(ps.Season!.Title, ps.Season.Id)).ToList(),

                        _dataContext.ProductBrands.Include(ps => ps.Brand).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.BrandViewModel(ps.Brand!.Name, ps.Brand.Id)).ToList(),

                              _dataContext.ProductGroups.Include(ps => ps.Group).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.GroupViewModel(ps.Group!.Title, ps.Group.Id)).ToList(),

                                    _dataContext.ProductUsageTimes.Include(ps => ps.UsageTime).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ShopViewModel.UsageTimeViewModel(ps.UsageTime!.Title, ps.UsageTime.Id)).ToList()
                );

            foreach (var item in model.Categories!)
            {
                model.Products = await _dataContext.ProductCategories.Include(p => p.Product)
                .Where(p => p.ProductId != product.Id).Where(p => p.CategoryId == item.Id)
                .Select(p => new ShopListItemViewModel
                (p.ProductId, p.Product!.Name, p.Product.Description, p.Product.Price, p.Product.CreatedAt,
                  p.Product.ProductImages!.Where(p => p.IsMain == true).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(p.Product.ProductImages!.Where(p => p.IsMain == true).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Products)
                 : String.Empty)).ToListAsync();
            }

            return View(model);
        }
    }
}
