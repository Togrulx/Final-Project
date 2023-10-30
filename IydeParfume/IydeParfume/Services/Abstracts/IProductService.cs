using IydeParfume.Areas.Admin.ViewModels.Product;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IydeParfume.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ListItemViewModel>> GetAllProduct();
        Task AddProduct(AddViewModel model);
        Task<bool> CheckProductSize(List<int> SizeIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductSeason(List<int> SeasonIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductUsageTime(List<int> UsageTimeIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductGroup(List<int> GroupIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductBrand(List<int> BrandIds, ModelStateDictionary ModelState);
        Task<bool> CheckProductCategory(List<int> CategoryIds, ModelStateDictionary ModelState);
        Task<UpdateViewModel> GetUpdatedProduct(Product product, int id);
        Task UpdateProduct(Product product, UpdateViewModel model);
    }
}
