using IydeParfume.Services.Abstracts;
using IydeParfume.Services.Concretes;

namespace IydeParfume.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
