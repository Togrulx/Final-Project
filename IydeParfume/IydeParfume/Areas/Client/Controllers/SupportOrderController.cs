using IydeParfume.Areas.Client.ViewModels.SupportOrder;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("SupportOrder")]
    public class SupportOrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportOrderController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("supportorderindex", Name = "client-supportOrder-index")]
        public async Task<IActionResult> Index()
        {
            var model =
              await
              _dataContext.SupportOrders.Select
              (c => new SupportOrderViewModel
              (c.Id, c.Title, c.Content, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }
    }
}
