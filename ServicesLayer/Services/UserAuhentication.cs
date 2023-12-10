using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesLayer.DTOs.Authentication.Requests;
using ServicesLayer.DTOs.Authentication.Responses;
using ServicesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services
{
    public class UserAuhentication : IUserAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        //private readonly JWT _jwt;
        public UserAuhentication(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration) //IOptions<JWT> jwt) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        
        public async Task<AuthenticationResponseDto> LoginUserAsync(LoginRequestDto model)
        {
            var response = new AuthenticationResponseDto();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                response.Message = "Email or Password is incorrect!";
                return response;
            }

            var UserToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            response.IsAuthenticated = true;
            response.Token = new JwtSecurityTokenHandler().WriteToken(UserToken);
          
            response.ExpiresOn = UserToken.ValidTo;

            return response;
        }


        public async Task<AuthenticationResponseDto> RegisterUserAsync(RegisterRequestDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthenticationResponseDto { Message = "This Email is already exist" };
            //if (await _userManager.FindByNameAsync(model.UserName) is not null)
            //    return new AuthenticationResponseDto { Message = "This User Name is already exist" };


            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthenticationResponseDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            var UserToken = await CreateJwtToken(user);

            return new AuthenticationResponseDto
            {
               
                ExpiresOn = UserToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(UserToken)

            };

        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var jwtConfig = _configuration.GetSection("JWT");
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(jwtConfig["DurationInDays"])),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
