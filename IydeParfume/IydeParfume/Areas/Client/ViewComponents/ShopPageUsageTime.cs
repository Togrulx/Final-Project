using IydeParfume.Areas.Client.ViewModels.ShopPage;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageUsageTime")]

    public class ShopPageUsageTime : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageUsageTime(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.UsageTimes
                .Select(c => new UsageTimeListItemViewModel(c.Id, c.Title!))
                .ToListAsync();

            return View(model);
        }
    }
}
