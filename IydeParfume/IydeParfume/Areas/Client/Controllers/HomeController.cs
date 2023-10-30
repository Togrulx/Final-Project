using IydeParfume.Areas.Client.ViewModels.Home;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dataContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("home-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dataContext.Sliders
                .Select(s => new SliderViewModel(s.Id, _fileService.GetFileUrl(s.BackgroundImageInFileSystem, UploadDirectory.Sliders)))
                .ToListAsync(),

                Categories = await _dataContext.Categories
                .OrderByDescending(c => c.Id).Take(4).Select(c => new CategoryViewModel(c.Id, c.Title!,
                _fileService.GetFileUrl(c.ImageInFileSystem, UploadDirectory.Categories))).ToListAsync(),
            };

            return View(model);
        }


        [HttpGet("indexsearch", Name = "client-homesearch-index")]
        public async Task<IActionResult> Search(string searchBy, string search, int? categoryId = null)
        {

            return RedirectToRoute("client-shoppage-index", new { searchBy = searchBy, search = search, categoryId = categoryId });

        }

    }
}
