using IydeParfume.Areas.Client.ViewComponents;
using IydeParfume.Areas.Client.ViewModels.Basket;
using IydeParfume.Areas.Client.ViewModels.Shop;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IBasketService _basketService;

        public BasketController(DataContext dbContext, IUserService userService, IBasketService basketService)
        {
            _dataContext = dbContext;
            _userService = userService;
            _basketService = basketService;
        }

        [HttpGet("index", Name = "client-basket-index")]
        public async Task<IActionResult> Index()
        {

            return View();
        }


        [HttpPost("add/{id}", Name = "client-basket-add")] 
        public async Task<IActionResult> AddProduct([FromRoute] int id, ShopViewModel model)
         {
            var product = await _dataContext.Products
                .Include(p => p.ProductSizes).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();
           
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product, model); 

            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(Basket), productCookiViewModel);
                //return RedirectToRoute("client-basket-index");


            }

            return ViewComponent(nameof(Basket), product);
        }

        [HttpGet("basket-delete/{productId}/{sizeId}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId, [FromRoute] int sizeId)
        {
            var productCookieViewModel = new List<BasketCookieViewModel>();

            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts
                   .Include(b => b.Basket).FirstOrDefaultAsync(bp => 
                   bp.Basket!.UserId == _userService.CurrentUser.Id &&
                   bp.ProductId == productId &&
                   bp.SizeId == sizeId);

                if (basketProduct is null) return NotFound();
            
                _dataContext.BasketProducts.Remove(basketProduct);
            }
            else
            {
                var product = await _dataContext.Products.
                    Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == productId);

                if (product is null) return NotFound();
              
                var productCookieValue = HttpContext.Request.Cookies["products"];

                if (productCookieValue is null) return NotFound();

                productCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);

                productCookieViewModel!.RemoveAll(pcvm => pcvm.Id == productId && pcvm.SizeId == sizeId);
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();

            //return ViewComponent(nameof(Basket), productCookieViewModel);
            return RedirectToRoute("client-basket-index");


        }
       

        [HttpGet("basket-individual-delete/{productId}/{sizeId}", Name = "client-individual-basket-delete")]
        public async Task<IActionResult> DeleteIndividualProduct([FromRoute] int productId, [FromRoute] int sizeId)
        {

            var productCookieViewModel = new List<BasketCookieViewModel>();
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                    .Include(p => p.Basket).FirstOrDefaultAsync(bp => bp.Basket!.UserId == _userService.CurrentUser.Id &&
                    bp.ProductId == productId && bp.SizeId == sizeId);

                if (basketProduct is null) return NotFound();


                if (basketProduct.Quantity > 1)
                {
                    basketProduct.Quantity -= 1;

                }
                else
                {
                    _dataContext.BasketProducts.Remove(basketProduct);
                }
            }
            else
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product is null) return NotFound();
               
                var productCookieValue = HttpContext.Request.Cookies["products"];

                if (productCookieValue is null) return NotFound();
         

                productCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);

                foreach (var cookieItem in productCookieViewModel!)
                {
                    if (cookieItem.Id == productId && cookieItem.SizeId == sizeId)
                    {
                        if (cookieItem.Quantity > 1)
                        {
                            cookieItem.Quantity -= 1;
                            cookieItem.Total = cookieItem.Quantity * cookieItem.Price;
                        }
                        else
                        {
                            productCookieViewModel.RemoveAll(p => p.Id == productId && p.SizeId == sizeId);
                            break;
                        }
                    }
                }
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));
            }
            await _dataContext.SaveChangesAsync();

            return ViewComponent(nameof(Basket), productCookieViewModel);
         

        }

        //[HttpPost("placeorder", Name = "client-order-placeorder")]
        //public async Task<IActionResult> PlaceOrder()
        //{
        //    var basketProducts = await _dataContext.BasketProducts.Include(p => p.Product)
        //        .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id).ToListAsync();

        //    var order = await CreateOrder();

        //    await CreateFullOrderProductAync(order, basketProducts);
        //    order.SumTotalPrice = order.OrderProducts.Sum(p => p.Total);

        //    await ResetBasketAsync(basketProducts);

        //    await _dataContext.SaveChangesAsync();


        //    return RedirectToRoute("client-account-order");


        //    async Task ResetBasketAsync(List<BasketProduct> basketProducts)
        //    {
        //        await Task.Run(() => _dataContext.RemoveRange(basketProducts));
        //    }

        //    async Task CreateFullOrderProductAync(Order order, List<BasketProduct> basketProducts)
        //    {
        //        foreach (var item in basketProducts)
        //        {
        //            var orderProduct = new OrderProduct
        //            {
        //                OrderId = order.Id,
        //                ProductId = item.ProductId,
        //                Price = item.Product.Price,
        //                Quantity = item.Quantity,
        //                Total = item.Product.Price * item.Quantity,
        //                SizeId = item.SizeId

        //            };
        //            await _dataContext.OrderProducts.AddAsync(orderProduct);
        //        }

        //    }

        //    async Task<Order> CreateOrder()
        //    {
        //        var order = new Order
        //        {
        //            Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
        //            UserId = _userService.CurrentUser.Id,
        //            Status = Database.Models.Enums.OrderStatus.Created
        //        };
        //        await _dbContext.Orders.AddAsync(order);



        //        return order;


        //    }
        //}
    }
}
