using Microsoft.EntityFrameworkCore;
using Student_Management_App_MVC.Models.Entities;

namespace Student_Management_App_MVC.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
    
}
