using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using IydeParfume.Areas.Admin.ViewModels.AboutUs;
using Microsoft.EntityFrameworkCore;
using IydeParfume.Database.Models;

using Microsoft.AspNetCore.Authorization;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/aboutUs")]
    [Authorize(Roles = "admin")]

    public class AboutUsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public readonly ILogger<BlogController> _logger;

        public AboutUsController(DataContext dataContext, IFileService fileService, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }


        #region List'

        [HttpGet("list", Name = "admin-aboutUs-list")]
        public async Task<IActionResult> List()
        {

            var model = await _dataContext.AboutUs.OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(p.Id, p.Title, p.Content, p.CreatedAt
                )).ToListAsync();


            return View(model);
        }

        #endregion

        #region Add' 

        [HttpGet("add", Name = "admin-aboutUs-add")]
        public async Task<IActionResult> Add()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-aboutUs-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-aboutUs-list");


            async void AddProduct()
            {
                var product = new AboutUs
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                };

                await _dataContext.AboutUs.AddAsync(product);

            }
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-aboutUs-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.AboutUs.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null) return NotFound();


            _dataContext.AboutUs.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-aboutUs-list");
        }


        #endregion
    }
}
