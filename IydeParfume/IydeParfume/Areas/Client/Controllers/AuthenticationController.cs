using IydeParfume.Services.Abstracts;
using IydeParfume.Areas.Client.Validation;
using IydeParfume.Areas.Client.ViewModels.Authentication;
using IydeParfume.Contracts.Identity;
using IydeParfume.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Register'

        [HttpGet("register", Name = "client-auth-register")]
        //[ServiceFilter(typeof(CurrentUserAtributeValidation))]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var emails = new List<string>();

            emails.Add(model.Email);

            await _userService.CreateAsync(model);


            return RedirectToRoute("client-home-index");
        }
        #endregion

        #region Login'

        [HttpGet("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }


            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password not correct");
                return View(model);
            }
            if (await _dataContext.Users.AnyAsync(u => u.Email == model.Email && u.Roles!.Name == RoleNames.ADMIN))
            {
                await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                return RedirectToRoute("admin-product-list");
            }

            await _userService.SignInAsync(model!.Email, model!.Password);


            return RedirectToRoute("client-home-index");
        }
        #endregion

        #region LogOut'

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }
        #endregion

        #region Activate'

        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> Activate([FromRoute] string token)
        {
            var userActivation = await _dataContext.UserActivations.Include(u => u.User)
                .FirstOrDefaultAsync(u => !u.User!.IsActive && u.ActivationToken == token);

            if (userActivation is null) return NotFound();

            if (DateTime.Now > userActivation.ExpiredDate) return Ok("Token expired sorry..");

            userActivation.User!.IsActive = true;

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("client-auth-login");
        }
        #endregion
    }
}
