using ServicesLayer.DTOs.Authentication.Requests;
using ServicesLayer.DTOs.Authentication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces
{
    public interface IUserAuthentication
    {
        Task<AuthenticationResponseDto> RegisterUserAsync(RegisterRequestDto model);
        Task<AuthenticationResponseDto> LoginUserAsync(LoginRequestDto model);
       // Task<string> AddRoleAsync(AddRolesRequest model);
    }
}
