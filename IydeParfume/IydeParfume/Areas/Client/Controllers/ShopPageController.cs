using IydeParfume.Areas.Client.ViewComponents;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shoppage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public ShopPageController(DataContext dataContext, IBasketService basketService,
            IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
            _fileService = fileService;
        }


        #region Index'

        [HttpGet("index", Name = "client-shoppage-index")]
        public IActionResult Index(string searchBy, string search, int? categoryId = null, int? startPriceId = null)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.SearchBy = searchBy;
            ViewBag.Search = search;
            ViewBag.StartPriceId = startPriceId;
            return View();
        }

        #endregion

        #region Filter'

        [HttpGet("filter", Name = "client-shoppage-filter")]
        public async Task<IActionResult> Sort(string? searchBy, string? search,
            [FromQuery] int? startPriceId,
            [FromQuery] int? sort = null,
            [FromQuery] int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            [FromQuery] int? seasonId = null,
            [FromQuery] int? brandId = null,
            [FromQuery] int? groupId = null,
            [FromQuery] int? usageTimeId = null
            ) 
        {

            return ViewComponent(nameof(ShopPageProduct), new { searchBy = searchBy, search = search, startPriceId = startPriceId, sort = sort,
                categoryId = categoryId, minPrice = minPrice, maxPrice = maxPrice,
                seasonId = seasonId,brandId=brandId,
            groupId=groupId,usageTimeId=usageTimeId});
        }

        #endregion
    }
}
