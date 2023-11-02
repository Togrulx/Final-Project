using IydeParfume.Areas.Client.ViewModels.Home;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Blog")]

    public class Blog : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =await _dataContext.Blogs
               .Include(b => b.BlogDisplays)

                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content,
                b.BlogDisplays!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogDisplays!
                .Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blog)
                : String.Empty,
              
                //b.BlogDisplays!.FirstOrDefault()!.IsImage!= null! ? b.BlogDisplays!.FirstOrDefault()!.IsImage : default ,
                //b.BlogDisplays!.FirstOrDefault()!.IsVidio!= null! ? b.BlogDisplays!.FirstOrDefault()!.IsVidio : default,
                b.CreatedAt
              
                )).ToListAsync();

            if (model is null) { };
            
                     
            
         
            return View(model);
        }
    }
}
