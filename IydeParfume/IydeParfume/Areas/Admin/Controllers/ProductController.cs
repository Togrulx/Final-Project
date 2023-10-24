using IydeParfume.Areas.Admin.ViewModels.Product;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin")]

    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IProductService _productService;

        public ProductController(DataContext dataContext, IFileService fileService, IProductService productService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _productService = productService;
        }

        #region List'

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List()
        {
            return View(await _productService.GetAllProduct());
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Catagories = await _dataContext.Categories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title!))
                    .ToListAsync(),

                Seasons = await _dataContext.Seasons
                .Select(t => new SeasonListItemViewModel(t.Id, t.Title))
                .ToListAsync(),

                UsageTimes = await _dataContext.UsageTimes
                .Select(t => new UsageTimeListItemViewModel(t.Id, t.Title))
                .ToListAsync(),

                Groups = await _dataContext.Groups
                .Select(t => new GroupListItemViewModel(t.Id, t.Title))
                .ToListAsync(),

                Brands = await _dataContext.Brands
                .Select(t => new BrandListItemViewModel(t.Id, t.Name))
                .ToListAsync(),

                Sizes = await _dataContext.Sizes
                .Select(S => new SizeListItemViewModel(S.Id, S.PrSize))
                .ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }
            var cehckCategory = await _productService.CheckProductCategory(model.CategoryIds, ModelState);
            var checkSeason = await _productService.CheckProductSeason(model.SeasonIds, ModelState);
            var checkUsageTime = await _productService.CheckProductUsageTime(model.UsageTimeIds, ModelState);
            var checkGroup = await _productService.CheckProductGroup(model.GroupIds, ModelState);
            var checkBrand = await _productService.CheckProductBrand(model.BrandIds, ModelState);
            var cehckSize = await _productService.CheckProductSize(model.SizeIds, ModelState);

            if (!cehckCategory || !checkSeason || !checkUsageTime || !checkGroup || !checkBrand || !cehckSize)
            {
                return GetView(model);
            }
            await _productService.AddProduct(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            IActionResult GetView(AddViewModel model)
            {

                model.Catagories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title!))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.PrSize))
                 .ToList();

                model.Seasons = _dataContext.Seasons
                 .Select(c => new SeasonListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.UsageTimes = _dataContext.UsageTimes
                 .Select(c => new UsageTimeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.Groups = _dataContext.Groups
                .Select(c => new GroupListItemViewModel(c.Id, c.Title))
                .ToList();

                model.Brands = _dataContext.Brands
                .Select(c => new BrandListItemViewModel(c.Id, c.Name))
                .ToList();


                return View(model);
            }
        }
        #endregion

        #region Update'

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductSeasons)
                .Include(p => p.ProductUsageTimes)
                .Include(p => p.ProductGroups)
                .Include(p => p.ProductBrands)
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }
            var model = await _productService.GetUpdatedProduct(product, id);
            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel? model)
        {
            var product = await _dataContext.Products
                    .Include(p => p.ProductSizes)
                    .Include(p => p.ProductSeasons)
                    .Include(p => p.ProductUsageTimes)
                    .Include(p => p.ProductGroups)
                    .Include(p => p.ProductBrands)
                    .Include(p => p.ProductCategories)
                    .Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == model!.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model!);
            }

            var cehckCategory = await _productService.CheckProductCategory(model!.CategoryIds, ModelState);
            var checkSeason = await _productService.CheckProductSeason(model.SeasonIds, ModelState);
            var checkUsageTime = await _productService.CheckProductUsageTime(model.UsageTimeIds, ModelState);
            var checkGroup = await _productService.CheckProductGroup(model.GroupIds, ModelState);
            var checkBrand = await _productService.CheckProductBrand(model.BrandIds, ModelState);
            var cehckSize = await _productService.CheckProductSize(model.SizeIds, ModelState);

            if (!cehckCategory || !checkSeason || !checkUsageTime || !checkGroup || !checkBrand || !cehckSize)
            {
                return GetView(model);
            }

            await _productService.UpdateProduct(product, model);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");


          
            IActionResult GetView(UpdateViewModel model)
            {
                model.ImagesUrl = product.ProductImages!
               .Where(p => p.IsMain == false)
               .Select(p => new UpdateViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Products)))
               .ToList();

                model.MainImgUrls = product.ProductImages!.Where(p => p.IsMain == true)
                 .Select(p => new UpdateViewModel.MainImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Products)))
                 .ToList();

                model.Catagories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title!))
                   .ToList();

                model.CategoryIds = product.ProductCategories!.Select(c => c.CategoryId)
                    .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.PrSize))
                 .ToList();

                model.SizeIds = product.ProductSizes!.Select(c => c.SizeId).ToList();

                model.Seasons = _dataContext.Seasons
                 .Select(c => new SeasonListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.SeasonIds = product.ProductSeasons!.Select(c => c.SeasonId).ToList();

                model.UsageTimes = _dataContext.UsageTimes
                 .Select(c => new UsageTimeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.UsageTimeIds = product.ProductUsageTimes!.Select(c => c.UsageTimeId).ToList();

                model.Groups = _dataContext.Groups
                 .Select(c => new GroupListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.GroupIds = product.ProductGroups!.Select(c => c.GroupId).ToList();

                model.Brands = _dataContext.Brands
                 .Select(c => new BrandListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.BrandIds = product.ProductBrands!.Select(c => c.BrandId).ToList();

              

                return View(model);
            }
           
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (products is null) return NotFound();
      

            foreach (var item in products.ProductImages!)
            {
                await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Products);
            }
            _dataContext.Products.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");
        }
        #endregion
    }
}
