using Student_Management_App_MVC.Models.Entities;

namespace Student_Management_App_MVC.DataBase.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string UserName);
        Task<User> AddUserAsync(User user);
        Task<bool> GetUserExistAsync(string UserName, string Email);
    }
}
