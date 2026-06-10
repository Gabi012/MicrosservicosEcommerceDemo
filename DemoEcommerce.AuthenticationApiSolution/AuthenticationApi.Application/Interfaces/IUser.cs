

using AuthenticationApi.Application.DTOs;
using ECommerce.SharedLibrary.Responses;

namespace AuthenticationApi.Application.Interfaces
{
    public interface IUser
    {
        public Task<Response> Register(AppUserDTO appUserDTO);
        public Task<Response> Login(LoginDTO loginDTO);
        public Task<GetUserDto> GetUser(int userId);
    }
}
