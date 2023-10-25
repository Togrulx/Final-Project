using IydeParfume.Areas.Admin.ViewModels.Slider;
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
    [Route("admin/slider")]
    [Authorize(Roles = "admin")]

    public class SliderController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        #region List'

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Sliders.Select(s => new ListItemViewModel(s.Id, _fileService
                .GetFileUrl(s.BackgroundImageInFileSystem, UploadDirectory.Sliders), s.CreatedAt, s.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var imageNameInSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.Sliders);

            AddSlider(model.BackgroundImage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-slider-list");

            async void AddSlider(string imageName, string imageNameInSystem)
            {
                var slider = new Slider
                {
                    BackgroundImage = imageName,
                    BackgroundImageInFileSystem = imageNameInSystem,
               
                };

                await _dataContext.Sliders.AddAsync(slider);
            }

        }

        #endregion

        #region Update'

        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = slider.Id,
                BackgroundImageUrl = _fileService.GetFileUrl(slider.BackgroundImageInFileSystem, UploadDirectory.Sliders),
          
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(model);


            await _fileService.DeleteAsync(model.BackgroundImage.FileName, UploadDirectory.Sliders);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.Sliders);


            await UpdateSliderImage(model.BackgroundImage.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-slider-list");


            async Task UpdateSliderImage(string imageName, string imageNameInSystem)
            {
                slider.BackgroundImage = imageName;
                slider.BackgroundImageInFileSystem = imageNameInSystem;
                slider.UpdatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null) return NotFound();

            await _fileService.DeleteAsync(slider.BackgroundImageInFileSystem, UploadDirectory.Sliders);
            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
        #endregion
    }
}
