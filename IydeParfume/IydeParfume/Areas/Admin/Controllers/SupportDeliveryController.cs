using IydeParfume.Areas.Admin.ViewModels.SupportDelivery;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("SupportDelivery")]
    [Authorize(Roles = "admin")]

    public class SupportDeliveryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportDeliveryController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("list", Name = "admin-SupportDelivery-list")]
        public async Task<IActionResult> List()
        {
            var model =
              await
              _dataContext.SupportDeliveries.Select
              (c => new ListItemViewModel
              (c.Id, c.Title, c.Content, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }

        #region Add' 

        [HttpGet("add", Name = "admin-SupportDelivery-add")]
        public async Task<IActionResult> Add()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-SupportDelivery-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-SupportDelivery-list");


            async void AddProduct()
            {
                var product = new SupportDelivery
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                };

                await _dataContext.SupportDeliveries.AddAsync(product);

            }
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-SupportDelivery-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dataContext.SupportDeliveries.FirstOrDefaultAsync(c => c.Id == id);

            if (model is null) return NotFound();


            _dataContext.SupportDeliveries.Remove(model);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-SupportDelivery-list");
        }
        #endregion
    }
}
