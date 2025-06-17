using Student_Management_App_MVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_App_MVC.Models.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
