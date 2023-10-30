using IydeParfume.Contracts.Email;

namespace IydeParfume.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
