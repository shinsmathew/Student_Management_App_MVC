using Microsoft.EntityFrameworkCore;
using Student_Management_App_MVC.DataBase.Repositories.Interfaces;
using Student_Management_App_MVC.Models.Entities;

namespace Student_Management_App_MVC.DataBase.Repositories.Implementations
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<User> AddUserAsync(User user)
        {
             _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> GetUserExistAsync(string userName, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == userName || u.Email == email);
        }
    }
    
}
