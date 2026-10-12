using DGE.Domain.Entities;
using DGE.Service.Shared.Interface;
using static DGE.Domain.Enums.EnumExtensions;

namespace DGE.Service.ServiceInterfaces
{
    public interface IOtpService : IBaseService<Otp>
    {
        Task<string?> GenerateOtpAsync(User user);
        Task<string> SendOtpAsync(User user, OtpReasonEnum reason);
        Task<bool> VerifyOtpAsync(User user, string otp);
        Task<string> MarkAsUsedAsync(string Otp, string Email);
    }
}
