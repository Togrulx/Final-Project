using IydeParfume.Contracts.Email;
using IydeParfume.Contracts.File;
using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IydeParfume.Areas.Admin.ViewModels.Order;


namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/orders")]
    [Authorize(Roles = "admin")]

    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public IEmailService _emailService;
        public OrderController(DataContext dataContext, IUserService userService, IEmailService emailService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _emailService = emailService;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Orders.Include(o => o.User)
               .Select(b => new ListItemViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice, b.User.Email))
               .ToListAsync();

            return View(model);
        }


        [HttpGet("list/{id}", Name = "admin-orderProduct-list")]
        public async Task<IActionResult> OrderProductList(string id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            var model = await _dataContext.OrderProducts.Where(o => o.OrderId == order.Id)
                .Select(op => new OrderProductListItemViewModel(op.Id, op.Product.ProductImages
                .Where(p => p.IsMain == true).FirstOrDefault() != null
              ? _fileService.GetFileUrl(op.Product.ProductImages
              .Where(p => p.IsMain == true).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Products)
              : String.Empty, op.Product.Name, op.Size.PrSize, op.Quantity, (int)op.Total)).ToListAsync();


            return View(model);
        }


        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> Update(string id)
        {
            var order = await _dataContext.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null) return NotFound();
          
            var model = new UpdateViewModel { Id = id };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> Update(string id, UpdateViewModel model)
        {
            var order = await _dataContext.Orders.Include(p => p.User).Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null) return NotFound();
           
            order.Status = model.Status;

            var stausMessageDto = PrepareStausMessage(order.User.Email);
            _emailService.Send(stausMessageDto);

        
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");
            MessageDto PrepareStausMessage(string email)
            {
                string body = "Order Has Been Updated";

                string subject = EmailMessages.Subject.NOTIFICATION_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }



        [HttpPost("delete/{id}", Name = "admin-order-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var orders = await _dataContext.Orders.Include(p => p.OrderProducts).FirstOrDefaultAsync(p => p.Id == id);
            if (orders is null)
            {
                return NotFound();
            }
            _dataContext.Orders.Remove(orders);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-order-list");
        }
       
    }
}
