using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using IydeParfume.Areas.Client.ViewModels.SupportDelivery;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("SupportDelivery")]
    public class SupportDeliveryController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportDeliveryController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("supportdeliveryindex", Name = "client-supportDelivery-index")]
        public async Task<IActionResult> Index()
        {
            var model =
              await
              _dataContext.SupportDeliveries.Select
              (c => new ListItemViewModel
              (c.Id, c.Title, c.Content, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }
    }
}
