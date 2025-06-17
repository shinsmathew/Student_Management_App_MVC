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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<bool> LoginUserAsync(UserLoginDto userDto)
        {
            var user = await _userRepository.GetUserByUserNameAsync(userDto.Username);
            if (user == null || !PasswordHasher.VerifyPassword(userDto.Password, user.Password))
                return false;

            await CookieAuthManager.SignInAsync(
                _httpContextAccessor.HttpContext,
                user.UserID,
                user.Username,
                user.Role.ToString());

            return true;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto userDto)
        {
            if (await _userRepository.GetUserExistAsync(userDto.Username, userDto.Email))
                return false;

            var user = _mapper.Map<User>(userDto);
            user.Password = PasswordHasher.HashPassword(userDto.Password);
            user.Role = Enum.Parse<UserRole>(userDto.Role);

            await _userRepository.AddUserAsync(user);
            return true;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUserNameAsync(username);
        }

        public async Task LogoutUserAsync()
        {
            await CookieAuthManager.SignOutAsync(_httpContextAccessor.HttpContext);
        }
    }
}