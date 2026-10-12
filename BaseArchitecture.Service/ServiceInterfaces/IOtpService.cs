using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Service.Shared.Interface;
using static BaseArchitecture.Domain.Enums.EnumExtensions;

namespace BaseArchitecture.Service.ServiceInterfaces
{
    public interface IOtpService : IBaseService<Otp>
    {
        Task<string?> GenerateOtpAsync(User user);
        Task<string> SendOtpAsync(User user, OtpReasonEnum reason);
        Task<bool> VerifyOtpAsync(User user, string otp);
        Task<string> MarkAsUsedAsync(string Otp, string Email);
    }
}
