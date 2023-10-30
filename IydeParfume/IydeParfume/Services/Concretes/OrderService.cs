using IydeParfume.Database;
using IydeParfume.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private const string ORDER_TRACKING_CODE = "OR";
        private const int ORDER_TRACKINH_MIN_RANGE = 10_000;
        private const int ORDER_TRACKINH_MAX_RANGE = 100_000;

        public OrderService(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<string> GenerateUniqueTrackingCodeAsync()
        {
            string token = string.Empty;
            do
            {
                token = GenerateRandomCode();

            } while (await _dataContext.Orders.AnyAsync(o => o.Id == token));

            return token;
        }
        private string GenerateRandomCode()
        {
            return $"{ORDER_TRACKING_CODE}{Random.Shared.Next(ORDER_TRACKINH_MIN_RANGE, ORDER_TRACKINH_MAX_RANGE)}";
        }
    }
}
