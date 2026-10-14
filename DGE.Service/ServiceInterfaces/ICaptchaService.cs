namespace DGE.Service.ServiceInterfaces
{
    public interface ICaptchaService
    {
        Task<bool> VerifyAsync(string? token, CancellationToken cancellationToken = default);
    }
}
