namespace IydeParfume.Services.Abstracts
{
    public interface IOrderService
    {
        Task<string> GenerateUniqueTrackingCodeAsync();

    }
}
