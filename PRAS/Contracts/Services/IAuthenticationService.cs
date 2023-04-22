using PRAS.DataTransferObjects;

namespace PRAS.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> SignInAsync(UserForAuthenticationDto userDto);
    }
}
