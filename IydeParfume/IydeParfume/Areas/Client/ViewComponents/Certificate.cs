using IydeParfume.Areas.Client.ViewModels.AboutUs;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Certificate")]

    public class Certificate : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Certificate(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Certificates.Select(pb => new CertificateViewModel(pb.Id, pb.Title, _fileService
                 .GetFileUrl(pb.FileNameInSystem, UploadDirectory.Certificates))).ToListAsync();

            return View(model);
        }
    }
}
