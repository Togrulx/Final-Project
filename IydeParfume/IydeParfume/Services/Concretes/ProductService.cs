using IydeParfume.Areas.Admin.Controllers;
using IydeParfume.Areas.Admin.ViewModels.Product;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;

        public ProductService(DataContext dataContext, IFileService fileService, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<List<ListItemViewModel>> GetAllProduct()
        {
            var model = await _dataContext.Products
              .Include(p => p.ProductImages).OrderByDescending(p => p.CreatedAt)
              .Select(p => new ListItemViewModel(p.Id, p.Name!, p.Description!, p.Price,
              p.ProductImages!.Where(p => p.IsMain == true).FirstOrDefault() != null
              ? _fileService.GetFileUrl(p.ProductImages!.Where(p => p.IsMain == true).FirstOrDefault()!
              .ImageNameFileSystem, UploadDirectory.Products)
              : String.Empty)).ToListAsync();

            return model;
        }

        public async Task AddProduct(AddViewModel model)
        {

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
              
            };

            await _dataContext.Products.AddAsync(product);

            if (model.MainImage is not null)
            {
                var imageNameInSystem = await _fileService.UploadAsync(model.MainImage, UploadDirectory.Products);


                var productImage = new ProductImage
                {
                    Product = product,
                    ImageNames = model.MainImage.FileName,
                    ImageNameFileSystem = imageNameInSystem,
                    IsMain = true,

                };
                await _dataContext.ProductImages.AddAsync(productImage);
            }

            if (model.AllImages is not null)
            {
                foreach (var image in model.AllImages!)
                {
                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Products);

                    var productAllImage = new ProductImage
                    {
                        Product = product,
                        ImageNames = image.FileName,
                        ImageNameFileSystem = allImageNameInSystem,
                        IsMain = false
                    };
                    await _dataContext.ProductImages.AddAsync(productAllImage);
                }
            }

            ///////////////////////////////////////////////////////////////////
            foreach (var catagoryId in model.CategoryIds)
            {
                var productCatagory = new ProductCategory
                {
                    CategoryId = catagoryId,
                    Product = product,
                };

                await _dataContext.ProductCategories.AddAsync(productCatagory);
            }
            foreach (var seasonId in model.SeasonIds)
            {
                var productSeason = new ProductSeason
                {
                    SeasonId = seasonId,
                    Product = product,
                };

                await _dataContext.ProductSeasons.AddAsync(productSeason);
            }
            foreach (var usageTimeId in model.UsageTimeIds)
            {
                var productUsageTime = new ProductUsageTime
                {
                    UsageTimeId = usageTimeId,
                    Product = product,
                };

                await _dataContext.ProductUsageTimes.AddAsync(productUsageTime);
            }
            foreach (var groupId in model.GroupIds)
            {
                var productGroup = new ProductGroup
                {
                    GroupId = groupId,
                    Product = product,
                };

                await _dataContext.ProductGroups.AddAsync(productGroup);
            }
            foreach (var brandId in model.BrandIds)
            {
                var productBrand = new ProductBrand
                {
                    BrandId = brandId,
                    Product = product,
                };

                await _dataContext.ProductBrands.AddAsync(productBrand);
            }

            foreach (var sizeId in model.SizeIds)
            {
                var productSize = new ProductSize
                {
                    SizeId = sizeId,
                    Product = product,
                };

                await _dataContext.ProductSizes.AddAsync(productSize);
            }
        }

        public async Task<bool> CheckProductSize(List<int> SizeIds, ModelStateDictionary ModelState)
        {
            foreach (var sizeId in SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckProductSeason(List<int> SeasonIds, ModelStateDictionary ModelState)
        {
            foreach (var seasonId in SeasonIds)
            {
                if (!await _dataContext.Seasons.AnyAsync(c => c.Id == seasonId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Season with id({seasonId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckProductUsageTime(List<int> UsageTimeIds, ModelStateDictionary ModelState)
        {
            foreach (var usageTimeId in UsageTimeIds)
            {
                if (!await _dataContext.UsageTimes.AnyAsync(c => c.Id == usageTimeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"UsageTime with id({usageTimeId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckProductGroup(List<int> GroupIds, ModelStateDictionary ModelState)
        {
            foreach (var groupId in GroupIds)
            {
                if (!await _dataContext.Groups.AnyAsync(c => c.Id == groupId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Group with id({groupId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckProductBrand(List<int> BrandIds, ModelStateDictionary ModelState)
        {
            foreach (var brandId in BrandIds)
            {
                if (!await _dataContext.Brands.AnyAsync(c => c.Id == brandId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({brandId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckProductCategory(List<int> CategoryIds, ModelStateDictionary ModelState)
        {
            foreach (var categoryId in CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return false;
                }
            }
            return true;
        }

        public async Task<UpdateViewModel> GetUpdatedProduct(Product product, int id)
        {
            var model = new UpdateViewModel
            {
                Id = product.Id,
                Name = product.Name!,
                Description = product.Description!,
                Price = product.Price,
             

                ImagesUrl = product.ProductImages!
                .Where(p => p.IsMain == false)
                .Select(p => new UpdateViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Products)))
                .ToList(),

                MainImgUrls = product.ProductImages!.Where(p => p.IsMain == true)
                .Select(p => new UpdateViewModel.MainImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Products)))
                .ToList(),

                Catagories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.ProductCategories!.Select(pc => pc.CategoryId).ToList(),

                Seasons = await _dataContext.Seasons.Select(s => new SeasonListItemViewModel(s.Id, s.Title)).ToListAsync(),
                SeasonIds = product.ProductSeasons!.Select(pc => pc.SeasonId).ToList(),

                UsageTimes = await _dataContext.UsageTimes.Select(s => new UsageTimeListItemViewModel(s.Id, s.Title)).ToListAsync(),
                UsageTimeIds = product.ProductUsageTimes!.Select(pc => pc.UsageTimeId).ToList(),

                Groups = await _dataContext.Groups.Select(s => new GroupListItemViewModel(s.Id, s.Title)).ToListAsync(),
                GroupIds = product.ProductGroups!.Select(pc => pc.GroupId).ToList(),

                Brands = await _dataContext.Brands.Select(s => new BrandListItemViewModel(s.Id, s.Name)).ToListAsync(),
                BrandIds = product.ProductBrands!.Select(pc => pc.BrandId).ToList(),

                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.PrSize)).ToListAsync(),
                SizeIds = product.ProductSizes!.Select(pc => pc.SizeId).ToList(),

            };

            return model;
        }

        public async Task UpdateProduct(Product product, UpdateViewModel model)
        {
           

            if (model.ProductImgIds is null)
            {
                foreach (var item in product.ProductImages!.Where(p => p.IsMain == false))
                {
                    await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Products);
                    _dataContext.ProductImages.Remove(item);
                    await _dataContext.SaveChangesAsync();
                }

            }
            if (model.ProductImgIds is not null)
            {
                var removedImg = product.ProductImages!.Where(p => p.IsMain == false).Where(pi => !model.ProductImgIds.Contains(pi.Id)).ToList();

                foreach (var item in removedImg)
                {
                    if (item.Id != 0)
                    {
                        var image = await _dataContext.ProductImages.FirstOrDefaultAsync(p => p.Id == item.Id);
                        await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Products);
                        _dataContext.ProductImages.Remove(item);
                        await _dataContext.SaveChangesAsync();
                    }
                }
            }


            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.UpdatedAt = DateTime.Now;
      

            #region Category

            var categoriesInDb = product.ProductCategories!.Select(bc => bc.CategoryId).ToList();
            var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
            var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

            product.ProductCategories!.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

            foreach (var categoryId in categoriesToAdd)
            {
                var productCatagory = new ProductCategory
                {
                    CategoryId = categoryId,
                    Product = product,
                };

                await _dataContext.ProductCategories.AddAsync(productCatagory);
            }
            #endregion

            #region Season

            var seasonInDb = product.ProductSeasons!.Select(bc => bc.SeasonId).ToList();
            var seasonToRemove = seasonInDb.Except(model.SeasonIds).ToList();
            var seasonToAdd = model.SeasonIds.Except(seasonInDb).ToList();

            product.ProductSeasons!.RemoveAll(bc => seasonToRemove.Contains(bc.SeasonId));


            foreach (var seasonId in seasonToAdd)
            {
                var productSeason = new ProductSeason
                {
                    SeasonId = seasonId,
                    Product = product,
                };

                await _dataContext.ProductSeasons.AddAsync(productSeason);
            }

            #endregion

            #region UsageTime

            var usageTimeInDb = product.ProductUsageTimes!.Select(bc => bc.UsageTimeId).ToList();
            var usageTimeToRemove = usageTimeInDb.Except(model.UsageTimeIds).ToList();
            var usageTimeToAdd = model.UsageTimeIds.Except(usageTimeInDb).ToList();

            product.ProductUsageTimes!.RemoveAll(bc => usageTimeToRemove.Contains(bc.UsageTimeId));


            foreach (var usageTimeId in usageTimeToAdd)
            {
                var productUsageTime = new ProductUsageTime
                {
                    UsageTimeId = usageTimeId,
                    Product = product,
                };

                await _dataContext.ProductUsageTimes.AddAsync(productUsageTime);
            }

            #endregion

            #region Group 

            var groupInDb = product.ProductGroups!.Select(bc => bc.GroupId).ToList();
            var groupToRemove = groupInDb.Except(model.GroupIds).ToList();
            var groupToAdd = model.GroupIds.Except(groupInDb).ToList();

            product.ProductGroups!.RemoveAll(bc => groupToRemove.Contains(bc.GroupId));


            foreach (var groupId in groupToAdd)
            {
                var productGroup = new ProductGroup
                {
                    GroupId = groupId,
                    Product = product,
                };

                await _dataContext.ProductGroups.AddAsync(productGroup);
            }

            #endregion

            #region Brand  

            var brandInDb = product.ProductBrands!.Select(bc => bc.BrandId).ToList();
            var brandToRemove = brandInDb.Except(model.BrandIds).ToList();
            var brandToAdd = model.BrandIds.Except(brandInDb).ToList();

            product.ProductBrands!.RemoveAll(bc => brandToRemove.Contains(bc.BrandId));


            foreach (var brandId in brandToAdd)
            {
                var productBrand = new ProductBrand
                {
                    BrandId = brandId,
                    Product = product,
                };

                await _dataContext.ProductBrands.AddAsync(productBrand);
            }

            #endregion


            #region Size

            var sizeInDb = product.ProductSizes!.Select(bc => bc.SizeId).ToList();
            var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
            var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

            product.ProductSizes!.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


            foreach (var sizeId in sizeToAdd)
            {
                var productSize = new ProductSize
                {
                    SizeId = sizeId,
                    Product = product,
                };

                await _dataContext.ProductSizes.AddAsync(productSize);
            }

            #endregion


            #region Images

            if (model.MainImage is not null)
            {
                var productImg = await _dataContext.ProductImages.Where(p => p.IsMain == true)
                    .FirstOrDefaultAsync(p => p.ProductId == product.Id);
                await _fileService.DeleteAsync(productImg!.ImageNameFileSystem, UploadDirectory.Products);
                _dataContext.ProductImages.Remove(productImg);

                var imageNameInSystem = await _fileService.UploadAsync(model.MainImage, UploadDirectory.Products);
                var productImage = new ProductImage
                {
                    Product = product,
                    ImageNames = model.MainImage.FileName,
                    ImageNameFileSystem = imageNameInSystem,
                    IsMain = true,

                };
                await _dataContext.ProductImages.AddAsync(productImage);
            }
            if (model.AllImages is not null)
            {
                foreach (var image in model.AllImages!)
                {
                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Products);

                    var productAllImage = new ProductImage
                    {
                        Product = product,
                        ImageNames = image.FileName,
                        ImageNameFileSystem = allImageNameInSystem,
                        IsMain = false
                    };
                    await _dataContext.ProductImages.AddAsync(productAllImage);
                }
            }
            #endregion
        }
    }
}
