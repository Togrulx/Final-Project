using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Client.Controllers
{
    [Area("client")]
    [Route("order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(DataContext dataContext, IFileService fileService, IUserService userService, IOrderService orderService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _userService = userService;
            _orderService = orderService;
        }
        [HttpPost("orderconfirmation", Name = "client-order-confirmation")]
        public async Task<IActionResult> PlaceOrder()
        {
            var basketProducts = await _dataContext.BasketProducts.Include(p => p.Product)
                .Where(p => p.Basket.UserId == _userService.CurrentUser.Id).ToListAsync();

            var order = await CreateOrder();

            await CreateFullOrderProductAync(order, basketProducts);
            order.SumTotalPrice = order.OrderProducts.Sum(p => p.Total);

            await ResetBasketAsync(basketProducts);

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("client-home-index");


            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dataContext.RemoveRange(basketProducts));
            }

            async Task CreateFullOrderProductAync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var item in basketProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        Total = item.Product.Price * item.Quantity,
                        SizeId = item.SizeId

                    };
                    await _dataContext.OrderProducts.AddAsync(orderProduct);
                }

            }

            async Task<Order> CreateOrder()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = Database.Models.Enums.OrderStatus.Created
                };
                await _dataContext.Orders.AddAsync(order);



                return order;


            }
        }
    }
}
