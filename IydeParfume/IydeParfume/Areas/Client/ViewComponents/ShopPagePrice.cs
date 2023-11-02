using IydeParfume.Areas.Client.ViewModels.ShopPage;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPagePrice")]

    public class ShopPagePrice : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPagePrice(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Categories
                .Select(c => new PriceListItemViewModel(c.Id, c.Title!))
                .ToListAsync();

            return View(model);
        }
    }
}
