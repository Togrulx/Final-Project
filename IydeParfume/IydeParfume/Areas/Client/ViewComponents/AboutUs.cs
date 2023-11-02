using IydeParfume.Areas.Client.ViewModels.Home;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "AboutUs")]

    public class AboutUs : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutUs(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.AboutUs
                .Include(b => b.AboutUsImages)

                 .Select(b => new AboutUsViewModel(b.Id, b.Title, b.Content,
                 b.AboutUsImages!.Take(1).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(b.AboutUsImages!
                 .Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.AboutUs)
                 : String.Empty, b.CreatedAt
                 )).ToListAsync();

            return View(model);
        }
    }
}
