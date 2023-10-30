using IydeParfume.Areas.Client.ViewModels.Contact;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public ContactController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("index", Name = "client-contact-index")]
        public async Task<IActionResult> Index()
        {
            return View(new SupportOrderViewModel());
        }

        [HttpPost("index", Name = "client-contact-index")]
        public async Task<IActionResult> Index(SupportOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = new Contact
            {
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            await _dataContext.Contacts.AddAsync(contact);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
