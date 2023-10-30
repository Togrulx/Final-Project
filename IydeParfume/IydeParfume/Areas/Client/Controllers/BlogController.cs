using IydeParfume.Areas.Client.ViewComponents;
using IydeParfume.Areas.Client.ViewModels.Blog;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public BlogController(DataContext dbContext, IFileService fileService)
        {
            _dataContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("blogindex",Name ="client-blog-index")]
        public async Task<IActionResult> Index()
        {
            var model = await _dataContext.Blogs
               .Include(b => b.BlogDisplays)

                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content,
                b.BlogDisplays!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogDisplays!
                .Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blog)
                : String.Empty,

                //b.BlogDisplays!.FirstOrDefault()!.IsImage != null! ? b.BlogDisplays!.FirstOrDefault()!.IsImage : default, //bura legv ele istirsense
                //b.BlogDisplays!.FirstOrDefault()!.IsVidio != null! ? b.BlogDisplays!.FirstOrDefault()!.IsVidio : default,
                b.CreatedAt

                )).ToListAsync();

            if (model is null) { };




            return View(model);
        }

        
    }
}
