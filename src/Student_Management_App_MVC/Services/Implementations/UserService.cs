using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Student_Management_App_MVC.DataBase.Repositories.Interfaces;
using Student_Management_App_MVC.Enums;
using Student_Management_App_MVC.Helpers;
using Student_Management_App_MVC.Models.DTOs.User;
using Student_Management_App_MVC.Models.Entities;
using Student_Management_App_MVC.Services.Interfaces;
using System.Security.Claims;
using AutoMapper;

namespace Student_Management_App_MVC.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var userExist = await _userRepository.GetUserExistAsync(userRegisterDto.Username, userRegisterDto.Email);
            
            if(userExist)
            {
                return false; // User already exists
            }
            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = PasswordHasher.HashPassword(userRegisterDto.Password);
            user.Role = Enum.Parse<UserRole>(userRegisterDto.Role);
            await _userRepository.AddUserAsync(user);
            return true; // User registered successfully
        }

        public async Task<bool> LoginUserAsync(UserLoginDto userLoginDto)
        {
           var user = await _userRepository.GetUserByUserNameAsync(userLoginDto.Username);
            if (user == null)
            {
                return false; // User not found
            }
            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.Role, user.Role.ToString())

            };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        public async Task LogoutUserAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
    
}
