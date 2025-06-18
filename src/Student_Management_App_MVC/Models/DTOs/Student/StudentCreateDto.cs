using System.ComponentModel.DataAnnotations;

namespace Student_Management_App_MVC.Models.DTOs.Student
{
    public class StudentCreateDto
    {
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone1 { get; set; } 
        public string StudentPhone2 { get; set; } 
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string SchoolName { get; set; }
        public string Course { get; set; }
    }
}
