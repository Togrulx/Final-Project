using FluentValidation.AspNetCore;
using FluentValidation;
using IydeParfume.Options;

namespace IydeParfume.Infrastructure.Configurations
{
    public static class OptionConfigurations
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //var emailconfig = configuration.getsection("emailconfiguration").get<emailconfigoptions>();
            //services.addsingleton(emailconfig);

            services.Configure<EmailConfigOptions>(configuration.GetSection(nameof(EmailConfigOptions)));
        }
    }
}
