using Student_Management_App_MVC.Models.DTOs.User;
using Student_Management_App_MVC.Models.Entities;

namespace Student_Management_App_MVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterDto userDto);
        Task<bool> LoginUserAsync(UserLoginDto userDto);
        Task<User> GetUserByUsernameAsync(string username);
        Task LogoutUserAsync();
    }
}
