using IydeParfume.Areas.Client.ViewModels.AboutUs;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("AboutUs")]
    public class AboutUsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public AboutUsController(DataContext dbContext, IFileService fileService)
        {
            _dataContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("aboutUsindex", Name = "client-aboutUs-index")]
        public async Task<IActionResult> Index()
        {
            var model = await _dataContext.AboutUs
            .Include(b => b.AboutUsImages)

             .Select(b => new AboutUsViewModel(b.Id, b.Title, b.Content,
             b.AboutUsImages!.Take(1).FirstOrDefault() != null
             ? _fileService.GetFileUrl(b.AboutUsImages!
             .Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.AboutUs)
             : String.Empty,b.CreatedAt
             )).ToListAsync();

            return View(model);
        }
    }
}
