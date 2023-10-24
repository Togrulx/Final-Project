using IydeParfume.Areas.Admin.ViewModels.AboutUsImage;
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
    [Route("admin/aboutUsImage")]
    [Authorize(Roles = "admin")]

    public class AboutUsImageController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public AboutUsImageController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        #region List'

        [HttpGet("{aboutUsId}/aboutUsImage/list", Name = "admin-aboutUsImage-list")]
        public async Task<IActionResult> List([FromRoute] int aboutUsId)
        {
            var product = await _dataContext.AboutUs
                .Include(p => p.AboutUsImages).FirstOrDefaultAsync(p => p.Id == aboutUsId);

            if (product == null) return NotFound();

            var model = new AboutUsImageViewModel { AboutUsId = product.Id };

            model.Files = product.AboutUsImages!.Select(p => new AboutUsImageViewModel.ListItem
            {
                Id = p.Id,
                FileUrl = _fileService.GetFileUrl(p.FileNameInSystem, Contracts.File.UploadDirectory.AboutUs),
          
                CreatedAt = p.CreatedAt
            }).ToList();

            return View(model);

        }
        #endregion

        #region Add'

        [HttpGet("{aboutUsId}/aboutUsImage/add", Name = "admin-aboutUsImage-add")]
        public async Task<IActionResult> Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{aboutUsId}/aboutUsImage/add", Name = "admin-aboutUsImage-add")]
        public async Task<IActionResult> Add([FromRoute] int aboutUsId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = await _dataContext.AboutUs.FirstOrDefaultAsync(p => p.Id == aboutUsId);

            if (product is null) return NotFound();


            var imageNameInSystem = await _fileService.UploadAsync(model.File, UploadDirectory.AboutUs);

            var productImage = new AboutUsImage
            {
                AboutUs = product,
                FileName = model.File.FileName,
                FileNameInSystem = imageNameInSystem,
          
            };

            await _dataContext.AboutUsImages.AddAsync(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-aboutUsImage-list", new { aboutUsId = aboutUsId });

        }
        #endregion

        #region Delete'

        [HttpPost("{aboutUsId}/aboutUsImage/{aboutUsFileId}/delete", Name = "admin-aboutUsImage-delete")]
        public async Task<IActionResult> Delete([FromRoute] int aboutUsId, [FromRoute] int aboutUsFileId)
        {

            var productImage = await _dataContext.AboutUsImages
                .FirstOrDefaultAsync(p => p.AboutUsId == aboutUsId && p.Id == aboutUsFileId);

            if (productImage is null) return NotFound();


            await _fileService.DeleteAsync(productImage.FileNameInSystem, UploadDirectory.AboutUs);

            _dataContext.AboutUsImages.Remove(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-aboutUsImage-list", new { aboutUsId = aboutUsId });

        }
        #endregion
    }
}
