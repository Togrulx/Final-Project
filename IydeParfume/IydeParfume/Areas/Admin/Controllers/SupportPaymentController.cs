using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IydeParfume.Areas.Admin.ViewModels.SupportPayment;
using Microsoft.AspNetCore.Authorization;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("SupportPayment")]
    [Authorize(Roles = "admin")]

    public class SupportPaymentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportPaymentController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("list", Name = "admin-SupportPayment-list")]
        public async Task<IActionResult> List()
        {
            var model =
              await
              _dataContext.SupportPayments.Select
              (c => new ListItemViewModel
              (c.Id, c.Title, c.Content,c.Note, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }

        #region Add' 

        [HttpGet("add", Name = "admin-SupportPayment-add")]
        public async Task<IActionResult> Add()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-SupportPayment-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-SupportPayment-list");


            async void AddProduct()
            {
                var product = new SupportPayment
                {
                    Title = model.Title,
                    Content = model.Content,
                    Note = model.Note,
                    CreatedAt = DateTime.Now,
                };

                await _dataContext.SupportPayments.AddAsync(product);

            }
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-SupportPayment-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dataContext.SupportPayments.FirstOrDefaultAsync(c => c.Id == id);

            if (model is null) return NotFound();


            _dataContext.SupportPayments.Remove(model);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-SupportPayment-list");
        }
        #endregion
    }
}
