using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Student_Management_App_MVC.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentName { get; set; }
        [Required]
        [MaxLength(100)]
        public string StudentAddress { get; set; }
        [Required]
        [MaxLength(50)]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [EmailAddress]
        public string StudentEmail { get; set; }
        [Required]
        [Phone]
        public string StudentPhone1 { get; set; }
        [Required]
        [Phone]
        public string StudentPhone2 { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(50)]
        public  string SchoolName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Course { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        

    }
}
