using IydeParfume.Database.Models;

namespace IydeParfume.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);

    }
}
