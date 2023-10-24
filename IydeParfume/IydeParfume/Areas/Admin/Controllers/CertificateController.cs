using IydeParfume.Areas.Admin.ViewModels.Certificate;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/certificate")]
    [Authorize(Roles = "admin")]

    public class CertificateController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public CertificateController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-certificate-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Certificates.Select(pb => new ListItemViewModel(pb.Id, pb.Title, _fileService
                .GetFileUrl(pb.FileNameInSystem, UploadDirectory.Certificates), pb.CreatedAt, pb.UpdatedAt)).ToListAsync();

            return View(model);
        }
        #endregion


        #region add

        [HttpGet("add", Name = "admin-certificate-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-certificate-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.Certificates);

            AddCertificate(model.BackgroundImage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-certificate-list");


            async void AddCertificate(string imageName, string imageNameInSystem)
            {
                var certificate = new Certificate
                {
                    Title = model.Title,
                    FileName = imageName,
                    FileNameInSystem = imageNameInSystem,
                };

                await _dataContext.Certificates.AddAsync(certificate);
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-certificate-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var certificate = await _dataContext.Certificates.FirstOrDefaultAsync(pb => pb.Id == id);
            if (certificate is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = certificate.Id,
                Title = certificate.Title,
                BackgroundImageInFileSystem = _fileService.GetFileUrl(certificate.FileNameInSystem, UploadDirectory.Certificates)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-certificate-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var certificate = await _dataContext.Certificates.FirstOrDefaultAsync(pb => pb.Id == model.Id);

            if (certificate is null) return NotFound();

            if (!ModelState.IsValid) return View(model);
           

            await _fileService.DeleteAsync(certificate.FileNameInSystem, UploadDirectory.Certificates);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.BackgroundImage!, UploadDirectory.Certificates);


            await UpdateCertificate(model.BackgroundImage!.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-certificate-list");


            async Task UpdateCertificate(string imageName, string imageNameInSystem)
            {
                certificate.Title = model.Title;
                certificate.FileName = imageName;
                certificate.FileNameInSystem = imageNameInSystem;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion


        #region Delete

        [HttpPost("delete/{id}", Name = "admin-certificate-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var paymentbenefit = await _dataContext.Certificates.FirstOrDefaultAsync(pb => pb.Id == id);
            if (paymentbenefit is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(paymentbenefit.FileNameInSystem, UploadDirectory.Certificates);
            _dataContext.Certificates.Remove(paymentbenefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-certificate-list");
        }
        #endregion
    }
}
