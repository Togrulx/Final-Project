using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using IydeParfume.Areas.Client.ViewModels.SupportPayment;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("SupportPayment")]
    public class SupportPaymentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportPaymentController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("supportpaymentindex", Name = "client-SupportPayment-index")]
        public async Task<IActionResult> Index()
        {
            var model =
              await
              _dataContext.SupportPayments.Select
              (c => new ListItemViewModel
              (c.Id, c.Title, c.Content,c.Note, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }
    }
}
